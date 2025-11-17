using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_web.Models;
using DoAn_web.ViewModels;// thu vien chua RegisterViewModel
using System.Web.Security;// thu vien cho ma hoa mat khau

namespace DoAn_web.Controllers
{
    public class AccountController : Controller
    {
        private MyStore2026Entities db = new MyStore2026Entities();
        // dang ki
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register( RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                // kiem tra username co ton tai ko
                var checkUser = db.Users.FirstOrDefault(u => u.Username == model.Username);
                if (checkUser != null)
                {
                    ModelState.AddModelError("", "Username này đã tồn tại.");
                    return View(model); // Trả về form và báo lỗi
                }
                // bat dau luu thong tin
                // tao user
                var user = new User();
                user.Username = model.Username;
                user.UserRole = "C"; // dat vai tro customer
                // ma hoa mat khau
                user.Password= System.Web.Helpers.Crypto.HashPassword( model.Password);
                db.Users.Add(user);
                // tao customer
                var customer = new Customer();
                customer.CustomerName = model.CustomerName;
                customer.CustomerPhone = model.CustomerPhone;
                customer.CustomerEmail = model.CustomerEmail;
                customer.CustomerAddress = model.CustomerAddress;
                customer.Username = model.Username; // lien ket khoa ngoai
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Username, string Password, string ReturnUrl) // <-- 🔥 THÊM "string ReturnUrl" VÀO ĐÂY
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                ViewBag.LoginError = "Vui lòng nhập Username và Mật khẩu.";
                return View();
            }

            var user = db.Users.FirstOrDefault(u => u.Username == Username);

            if (user != null)
            {
                bool isPasswordCorrect = System.Web.Helpers.Crypto.VerifyHashedPassword(user.Password, Password);

                if (isPasswordCorrect)
                {
                    // ... (Code tạo ticket và cookie) ...
                    var ticket = new FormsAuthenticationTicket(
                        1, user.Username, DateTime.Now, DateTime.Now.AddMinutes(30), true, user.UserRole.Trim()
                    );
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);
                    var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    // 🔥 THÊM KHỐI KIỂM TRA NÀY VÀO
                    // (Nếu có ReturnUrl và nó là link nội bộ, trả họ về đó)
                    if (!String.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        // Nếu không, về trang chủ
                        return RedirectToAction("Index", "Default");
                    }
                }
            }

            ViewBag.LoginError = "Username hoặc mật khẩu không chính xác.";
            return View();
        }

        // dang xuat
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); // xoa (Cookie)
            return RedirectToAction("Index", "Default"); 
        }
        // giai phong bo nho
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }










        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}