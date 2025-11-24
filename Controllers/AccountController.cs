using DoAn_web.Models;
using DoAn_web.ViewModels;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
// QUAN TRỌNG: Thêm thư viện này để bắt lỗi Validation chi tiết
using System.Data.Entity.Validation;

namespace DoAn_web.Controllers
{
    public class AccountController : Controller
    {
        private MyStore2026Entities db = new MyStore2026Entities();

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var checkUser = db.Users.FirstOrDefault(u => u.Username == model.Username);
                if (checkUser != null)
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại.");
                    return View(model);
                }

                try
                {
                    // 1. Tạo User
                    var user = new User();
                    user.Username = model.Username;
                    user.Password = model.Password;
                    user.UserRole = "C"; // Đảm bảo DB cột này cho phép lưu chữ "C"

                    db.Users.Add(user);
                    db.SaveChanges(); // Lưu User lần 1

                    // 2. Tạo Customer
                    var customer = new Customer();
                    customer.CustomerName = model.CustomerName;
                    customer.CustomerPhone = model.CustomerPhone;
                    customer.CustomerEmail = model.CustomerEmail;
                    customer.CustomerAddress = model.CustomerAddress;
                    customer.Username = model.Username;

                    db.Customers.Add(customer);
                    db.SaveChanges(); // Lưu Customer lần 2

                    return RedirectToAction("Login");
                }
                catch (DbEntityValidationException ex)
                {
                    // Bắt lỗi Validation cụ thể từ Entity Framework
                    foreach (var x in ex.EntityValidationErrors)
                    {
                        foreach (var error in x.ValidationErrors)
                        {
                            ModelState.AddModelError("", "Lỗi DB: " + error.ErrorMessage);
                        }
                    }
                    // Nếu lỡ tạo user rồi thì xóa đi để tránh rác
                    var u = db.Users.Find(model.Username);
                    if (u != null) { db.Users.Remove(u); db.SaveChanges(); }

                    return View(model);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Lỗi hệ thống: " + ex.Message);
                    // Nếu lỡ tạo user rồi thì xóa đi
                    var u = db.Users.Find(model.Username);
                    if (u != null) { db.Users.Remove(u); db.SaveChanges(); }

                    return View(model);
                }
            }
            return View(model);
        }
        

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Username, string Password, string ReturnUrl)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ViewBag.LoginError = "Vui lòng nhập Username và Mật khẩu.";
                return View();
            }

            var user = db.Users.FirstOrDefault(u => u.Username == Username);

            if (user != null)
            {
                if (user.Password == Password)
                {
                    var ticket = new FormsAuthenticationTicket(
                        1,
                        user.Username,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        true,
                        user.UserRole.Trim()
                    );

                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    if (!String.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Default");
                    }
                }
            }

            ViewBag.LoginError = "Username hoặc mật khẩu không chính xác.";
            return View();
        }

        // Action Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index", "Default");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}