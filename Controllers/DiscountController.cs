using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using DoAn_web.Models;

namespace DoAn_web.Areas.Admin2026.Controllers
{
    public class DiscountController : Controller
    {
        private MyStore2026Entities db = new MyStore2026Entities();

        // 1. DANH SÁCH MÃ GIẢM GIÁ
        public ActionResult Index()
        {
            var discounts = db.Discounts.OrderByDescending(d => d.EndDate).ToList();
            return View(discounts);
        }

        // 2. TẠO MỚI (GET)
        public ActionResult Create()
        {
            return View();
        }

        // 3. TẠO MỚI (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Discount discount)
        {
            // --- VALIDATION LOGIC ---

            // Check 1: Trùng mã Coupon
            if (db.Discounts.Any(d => d.CouponCode == discount.CouponCode))
            {
                ModelState.AddModelError("CouponCode", "Mã giảm giá này đã tồn tại!");
            }

            // Check 2: Logic giảm giá %
            if (discount.DiscountType == "Percentage" && discount.DiscountValue > 100)
            {
                ModelState.AddModelError("DiscountValue", "Giảm giá theo phần trăm không được quá 100%.");
            }

            // Check 3: Ngày tháng
            if (discount.StartDate >= discount.EndDate)
            {
                ModelState.AddModelError("EndDate", "Ngày kết thúc phải sau ngày bắt đầu.");
            }

            // Check 4: Số âm
            if (discount.DiscountValue <= 0) ModelState.AddModelError("DiscountValue", "Giá trị giảm phải lớn hơn 0.");
            if (discount.MinimumOrderAmount < 0) ModelState.AddModelError("MinimumOrderAmount", "Đơn tối thiểu không được âm.");


            if (ModelState.IsValid)
            {
                // Chuyển mã thành chữ in hoa cho chuẩn
                discount.CouponCode = discount.CouponCode.ToUpper();

                db.Discounts.Add(discount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discount);
        }

        // 4. SỬA (GET)
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Discount discount = db.Discounts.Find(id);
            if (discount == null) return HttpNotFound();
            return View(discount);
        }

        // 5. SỬA (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Discount discount)
        {
            // Check trùng mã (trừ chính nó ra)
            if (db.Discounts.Any(d => d.CouponCode == discount.CouponCode && d.DiscountID != discount.DiscountID))
            {
                ModelState.AddModelError("CouponCode", "Mã giảm giá này đã tồn tại!");
            }

            if (discount.DiscountType == "Percentage" && discount.DiscountValue > 100)
            {
                ModelState.AddModelError("DiscountValue", "Giảm giá theo phần trăm không được quá 100%.");
            }

            if (ModelState.IsValid)
            {
                discount.CouponCode = discount.CouponCode.ToUpper();
                db.Entry(discount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discount);
        }

        // 6. XÓA
        // Trong DiscountController.cs
        public ActionResult Delete(int id)
        {
            Discount discount = db.Discounts.Find(id);
            if (discount != null)
            {
                db.Discounts.Remove(discount);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}