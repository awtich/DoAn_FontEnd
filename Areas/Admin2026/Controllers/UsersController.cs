using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DoAn_web.Models;

namespace DoAn_web.Areas.Admin2026.Controllers
{
    public class UsersController : Controller
    {
        private MyStore2026Entities db = new MyStore2026Entities();

        // GET: Admin2026/Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Admin2026/Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Admin2026/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin2026/Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Admin2026/Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        // 1. CHỈ Bind "Username" và "Password". 
        //    Chúng ta sẽ tự gán UserRole
        public ActionResult Create([Bind(Include = "Username,Password")] User user)
        {
            // 2. Kiểm tra xem Username đã tồn tại trong database chưa
            if (db.Users.Any(u => u.Username == user.Username))
            {
                ModelState.AddModelError("Username", "Tên đăng nhập này đã tồn tại.");
            }

            // 3. Các quy tắc khác (mật khẩu 7 ký tự, có số) 
            //    sẽ được tự động kiểm tra bởi "ModelState.IsValid"

            if (ModelState.IsValid)
            {
                // 
                // ---- BẮT ĐẦU LOGIC MỚI CỦA BẠN ----
                //
                // 4. Kiểm tra 3 ký tự cuối của Username
                if (user.Username.EndsWith("@#$"))
                {
                    user.UserRole = "1"; // Giả sử UserRole là kiểu string
                }
                else
                {
                    user.UserRole = "0"; // Giả sử UserRole là kiểu string
                }
                //
                // ---- KẾT THÚC LOGIC MỚI ----
                //

                // 5. (NHẮC LẠI BẢO MẬT) Bạn PHẢI mã hóa mật khẩu trước khi lưu
                // Ví dụ: user.Password = MaHoaMatKhau(user.Password);

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Nếu có lỗi (trùng tên, sai mật khẩu), quay lại form
            return View(user);
        }

        // GET: Admin2026/Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin2026/Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Username,Password,UserRole")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Admin2026/Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Admin2026/Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
