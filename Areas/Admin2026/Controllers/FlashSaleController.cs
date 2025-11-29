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
    public class FlashSaleController : Controller
    {
        // GET: Admin2026/FlashSale
        private MyStore2026Entities db = new MyStore2026Entities();

        public ActionResult Index()
        {
            var flashSaleItems = db.FlashSaleItems.Include(f => f.Product)
                                   .OrderByDescending(f => f.StartDate)
                                   .ToList();
            return View(flashSaleItems);
        }
        public ActionResult Create()
        {
            // Lấy nguyên list Product (đừng select new nữa)
            ViewBag.ProductList = db.Products.OrderByDescending(p => p.ProductID).ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FlashSaleItem flashSaleItem)
        {
            // 1. TÌM SẢN PHẨM ĐỂ SO SÁNH GIÁ
            var product = db.Products.Find(flashSaleItem.ProductID);

            if (product != null)
            {
                // NẾU GIÁ SALE >= GIÁ GỐC -> BÁO LỖI VÀO MODELSTATE
                if (flashSaleItem.SalePrice >= product.ProductPrice)
                {
                    ModelState.AddModelError("SalePrice", "Giá Sale phải nhỏ hơn giá gốc (" + String.Format("{0:N0}", product.ProductPrice) + ")");
                }

                // KIỂM TRA SỐ LƯỢNG
                if (flashSaleItem.SaleQuantityLimit > product.Quantity)
                {
                    ModelState.AddModelError("SaleQuantityLimit", "Số lượng bán vượt quá tồn kho.");
                }
            }

            // 2. KIỂM TRA NGÀY THÁNG
            if (flashSaleItem.StartDate >= flashSaleItem.EndDate)
            {
                ModelState.AddModelError("EndDate", "Ngày kết thúc phải sau ngày bắt đầu.");
            }

            // 3. KIỂM TRA TỔNG THỂ
            if (!ModelState.IsValid)
            {
                // 1. Nạp lại danh sách sản phẩm (Code cũ của bạn)
                ViewBag.ProductList = db.Products.OrderByDescending(p => p.ProductID).ToList();

                // 2. --- THÊM ĐOẠN NÀY: TÌM GIÁ GỐC CỦA SẢN PHẨM ĐANG CHỌN ---
                var selectedProduct = db.Products.Find(flashSaleItem.ProductID);
                ViewBag.SelectedOriginPrice = selectedProduct != null ? selectedProduct.ProductPrice : 0;
                // -------------------------------------------------------------

                return View(flashSaleItem);
            }

            // 4. LƯU THÀNH CÔNG
            flashSaleItem.SoldQuantity = 0;
            db.FlashSaleItems.Add(flashSaleItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
{
    if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
    
    FlashSaleItem flashSaleItem = db.FlashSaleItems.Find(id);
    if (flashSaleItem == null) return HttpNotFound();

    // --- DÒNG QUAN TRỌNG CẦN THÊM ---
    // Nạp thông tin Product để lấy tên và giá gốc hiển thị ra View
    db.Entry(flashSaleItem).Reference(f => f.Product).Load(); 
    // ---------------------------------

    return View(flashSaleItem);
}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlashSaleItemID,ProductID,SalePrice,SaleQuantityLimit,SoldQuantity,StartDate,EndDate,IsActive")] FlashSaleItem flashSaleItem)
        {
            // 1. Check số lượng > 0
            if (flashSaleItem.SaleQuantityLimit <= 0)
            {
                ModelState.AddModelError("SaleQuantityLimit", "Số lượng bán Flash Sale phải lớn hơn 0.");
            }

            // 2. Check giá > 0
            if (flashSaleItem.SalePrice <= 0)
            {
                ModelState.AddModelError("SalePrice", "Giá Sale phải lớn hơn 0.");
            }

            // 3. Check thời gian
            if (flashSaleItem.StartDate >= flashSaleItem.EndDate)
            {
                ModelState.AddModelError("EndDate", "Ngày kết thúc phải lớn hơn ngày bắt đầu.");
            }

            // 4. Logic sản phẩm
            var product = db.Products.Find(flashSaleItem.ProductID);
            if (product != null)
            {
                if (flashSaleItem.SalePrice > 0 && flashSaleItem.SalePrice >= product.ProductPrice)
                {
                    ModelState.AddModelError("SalePrice", "Giá Sale phải thấp hơn giá gốc (" + product.ProductPrice.ToString("N0") + ").");
                }

                // Check tồn kho
                if (flashSaleItem.SaleQuantityLimit > product.Quantity)
                {
                    ModelState.AddModelError("SaleQuantityLimit", "Số lượng mở bán vượt quá tồn kho (" + product.Quantity + ").");
                }
            }

            // 5. Check trùng lịch (trừ chính nó)
            bool isDuplicateTime = db.FlashSaleItems.Any(f => f.ProductID == flashSaleItem.ProductID
                                                           && f.IsActive == true
                                                           && f.FlashSaleItemID != flashSaleItem.FlashSaleItemID
                                                           && f.StartDate < flashSaleItem.EndDate
                                                           && f.EndDate > flashSaleItem.StartDate);
            if (isDuplicateTime)
            {
                ModelState.AddModelError("", "Lịch này bị trùng với một Flash Sale khác.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(flashSaleItem).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "ProductName", flashSaleItem.ProductID);
            return View(flashSaleItem);
        }
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            FlashSaleItem flashSaleItem = db.FlashSaleItems.Find(id);
            if (flashSaleItem == null) return HttpNotFound();

            // Thực hiện xóa
            db.FlashSaleItems.Remove(flashSaleItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

    }
}