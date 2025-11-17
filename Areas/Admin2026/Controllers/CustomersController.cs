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
   
    public class CustomersController : Controller
    {
        private MyStore2026Entities db = new MyStore2026Entities();

        // GET: Admin2026/Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.User);
            return View(customers.ToList());
        }

        // GET: Admin2026/Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Admin2026/Customers/Create
        public ActionResult Create()
        {
            // Đảm bảo ở đây là "Username", "Username"
            ViewBag.Username = new SelectList(db.Users, "Username", "Username");
            return View();
        }

        // POST: Admin2026/Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Admin2026/Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,CustomerName,CustomerPhone,CustomerEmail,CustomerAddress,Username")] Customer customer)
        {
            // 1. Kiểm tra SĐT đã tồn tại chưa
            if (db.Customers.Any(c => c.CustomerPhone == customer.CustomerPhone))
            {
                ModelState.AddModelError("CustomerPhone", "Số điện thoại này đã được sử dụng");
            }

            // 2. Kiểm tra Email đã tồn tại chưa
            if (db.Customers.Any(c => c.CustomerEmail == customer.CustomerEmail))
            {
                ModelState.AddModelError("CustomerEmail", "Email này đã được sử dụng");
            }

            // 
            // ---- THÊM MỚI LOGIC NÀY ----
            //
            // 3. Kiểm tra Username (Tài khoản) đã được liên kết chưa
            if (db.Customers.Any(c => c.Username == customer.Username))
            {
                ModelState.AddModelError("Username", "Tài khoản này đã được liên kết với một khách hàng khác");
            }
            // ---- KẾT THÚC THÊM MỚI ----
            //

            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Username = new SelectList(db.Users, "Username", "Username", customer.Username);
            return View(customer);
        }

        // GET: Admin2026/Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(db.Users, "Username", "Username", customer.Username);
            return View(customer);
        }

        // POST: Admin2026/Customers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,CustomerName,CustomerPhone,CustomerEmail,CustomerAddress,Username")] Customer customer)
        {
            // 1. Kiểm tra SĐT (loại trừ chính khách hàng này)
            if (db.Customers.Any(c => c.CustomerPhone == customer.CustomerPhone && c.CustomerID != customer.CustomerID))
            {
                ModelState.AddModelError("CustomerPhone", "Số điện thoại này đã được sử dụng bởi một tài khoản khác");
            }

            // 2. Kiểm tra Email (loại trừ chính khách hàng này)
            if (db.Customers.Any(c => c.CustomerEmail == customer.CustomerEmail && c.CustomerID != customer.CustomerID))
            {
                ModelState.AddModelError("CustomerEmail", "Email này đã được sử dụng bởi một tài khoản khác");
            }

            // 
            // ---- THÊM MỚI LOGIC NÀY ----
            //
            // 3. Kiểm tra Username (loại trừ chính khách hàng này)
            if (db.Customers.Any(c => c.Username == customer.Username && c.CustomerID != customer.CustomerID))
            {
                ModelState.AddModelError("Username", "Tài khoản này đã được liên kết với một khách hàng khác");
            }
            // ---- KẾT THÚC THÊM MỚI ----
            //

            if (ModelState.IsValid)
            {
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Username = new SelectList(db.Users, "Username", "Password", customer.Username);
            return View(customer);
        }

        // GET: Admin2026/Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Admin2026/Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
