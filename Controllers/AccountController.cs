using DoAn_web.Models;
using DoAn_web.ViewModels; // Thư viện chứa RegisterViewModel
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security; // Thư viện cho FormsAuthentication

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
                // Bước 1: Kiểm tra Username đã tồn tại
                var checkUser = db.Users.FirstOrDefault(u => u.Username == model.Username);
                if (checkUser != null)
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại.");
                    return View(model);
                }

                // Bước 2: LƯU THÔNG TIN (KHÔNG MÃ HÓA MẬT KHẨU)

                // Tạo User
                var user = new User();
                user.Username = model.Username;
                user.UserRole = "C"; // Vai trò Customer
                //  XÓA MÃ HÓA: Lưu mật khẩu dưới dạng PLAIN TEXT 
                user.Password = model.Password;
                db.Users.Add(user);

                // Tạo Customer
                var customer = new Customer();
                customer.CustomerName = model.CustomerName;
                customer.CustomerPhone = model.CustomerPhone;
                customer.CustomerEmail = model.CustomerEmail;
                customer.CustomerAddress = model.CustomerAddress;
                customer.Username = model.Username;
                db.Customers.Add(customer);

                db.SaveChanges();
                return RedirectToAction("Login");
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
                // XÓA GIẢI MÃ: So sánh mật khẩu PLAIN TEXT trực tiếp 
                bool isPasswordCorrect = (user.Password == Password);

                if (isPasswordCorrect)
                {
                    // Code tạo ticket và cookie
                    var ticket = new FormsAuthenticationTicket(
                        1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, user.UserRole.Trim()
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

        // Giải phóng bộ nhớ
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Account/Index
        public ActionResult Index()
        {
            return View();
        }
    }
}