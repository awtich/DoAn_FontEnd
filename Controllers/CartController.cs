// File: /Controllers/CartController.cs
using DoAn_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace DoAn_web.Controllers
{
    public class CartController : Controller
    {
        private MyStore2026Entities db = new MyStore2026Entities();

        // Lấy giỏ hàng từ Session
        private List<CartItem> GetCart()
        {
            List<CartItem> cart = Session["Cart"] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // Lưu giỏ hàng vào Session
        private void SaveCart(List<CartItem> cart)
        {
            Session["Cart"] = cart;
        }

        // ===== HÀM DÙNG CHUNG: THÊM 1 SẢN PHẨM VÀO GIỎ =====
        // Trả về true nếu thêm OK, false nếu không đủ hàng / lỗi
        private bool AddItemToCart(int productID, int quantity)
        {
            List<CartItem> cart = GetCart();
            CartItem item = cart.Where(i => i != null)
                                .FirstOrDefault(i => i.ProductID == productID);

            var productDB = db.Products.Find(productID);

            // Tính số lượng sau khi cộng thêm
            int requiredQuantity = quantity;
            if (item != null)
            {
                requiredQuantity = item.Quantity + quantity;
            }

            // Kiểm tra tồn kho
            if (productDB == null || productDB.Quantity < requiredQuantity)
            {
                TempData["Error"] = $"Sản phẩm {productDB?.ProductName ?? "này"} chỉ còn {productDB?.Quantity ?? 0} sản phẩm. Vui lòng giảm số lượng.";
                return false;
            }

            // Nếu đã có trong giỏ → tăng số lượng
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                // Nếu chưa có → tạo item mới
                var newItem = new CartItem
                {
                    ProductID = productDB.ProductID,
                    ProductName = productDB.ProductName,
                    ProductImage = productDB.ProductImage,
                    Quantity = quantity,
                    UnitPrice = productDB.ProductPrice
                };
                cart.Add(newItem);
            }

            SaveCart(cart);
            return true;
        }
        // ================== HẾT HÀM DÙNG CHUNG ==================

        // GET: /Cart/Index
        public ActionResult Index()
        {
            List<CartItem> cart = GetCart();

            if (cart.Count == 0)
            {
                ViewBag.CartMessage = "Giỏ hàng của bạn đang trống.";
            }

            return View(cart);
        }

        // POST: /Cart/AddToCart  (thêm 1 sản phẩm)
        [HttpPost]
        public ActionResult AddToCart(int productID, int quantity)
        {
            bool ok = AddItemToCart(productID, quantity);
            // Nếu lỗi tồn kho thì TempData["Error"] đã có message
            return RedirectToAction("Index");
        }

        // POST: /Cart/AddComboToCart  (máy + 3 phụ kiện)
        [HttpPost]
        public ActionResult AddComboToCart(int productID, int[] accessoryIds)
        {
            // Thêm sản phẩm chính
            if (!AddItemToCart(productID, 1))
            {
                return RedirectToAction("Index");
            }

            // Thêm từng phụ kiện
            if (accessoryIds != null)
            {
                foreach (var accId in accessoryIds)
                {
                    if (!AddItemToCart(accId, 1))
                    {
                        // Nếu có phụ kiện thiếu hàng thì dừng lại
                        break;
                    }
                }
            }

            return RedirectToAction("Index");
        }

        // GET: /Cart/RemoveFromCart/5
        public ActionResult RemoveFromCart(int productID)
        {
            List<CartItem> cart = GetCart();
            CartItem item = cart.Where(i => i != null)
                                .FirstOrDefault(i => i.ProductID == productID);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ChangeQuantity(int productID, int change)
        {
            List<CartItem> cart = GetCart();
            CartItem item = cart.Where(i => i != null)
                                .FirstOrDefault(i => i.ProductID == productID);

            if (item == null)
                return RedirectToAction("Index");

            var productDB = db.Products.Find(productID);
            if (productDB == null)
                return RedirectToAction("Index");

            int newQuantity = item.Quantity + change;

            if (newQuantity <= 0)
            {
                cart.Remove(item);
                SaveCart(cart);
                return RedirectToAction("Index");
            }

            if (newQuantity > productDB.Quantity)
            {
                TempData["Error"] = $"Sản phẩm {productDB.ProductName} chỉ còn {productDB.Quantity} sản phẩm. Vui lòng giảm số lượng.";
                return RedirectToAction("Index");
            }

            item.Quantity = newQuantity;
            SaveCart(cart);

            return RedirectToAction("Index");
        }


        // Dọn dẹp DbContext
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private int GetCurrentCustomerID()
        {
            var username = User.Identity.Name;
            var customer = db.Customers.FirstOrDefault(c => c.Username == username);
            if (customer != null)
            {
                return customer.CustomerID;
            }
            return 0;
        }

        [Authorize(Roles = "C")]
        public ActionResult Checkout()
        {
            List<CartItem> cart = GetCart();
            if (cart.Count == 0 || cart.Any(i => i == null))
            {
                return RedirectToAction("Index");
            }

            int customerID = GetCurrentCustomerID();
            var customer = db.Customers.Find(customerID);

            ViewBag.Cart = cart;
            ViewBag.Customer = customer;

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "C")]
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(string AddressDelivery, decimal TotalAmount, string PaymentMethod)
        {
            List<CartItem> cart = GetCart();
            if (cart.Count == 0 || cart.Any(i => i == null))
            {
                return RedirectToAction("Index");
            }

            try
            {
                var order = new Order();
                order.CustomerID = GetCurrentCustomerID();
                order.OrderDate = DateTime.Now;
                order.TotalAmount = TotalAmount;
                order.AddressDelivery = AddressDelivery;

                order.PaymentStatus = (PaymentMethod == "Tiền mặt (COD)")
                                      ? "Chờ xử lý"
                                      : "Chờ xác nhận chuyển khoảng";

                order.ShippingStatus = "Chờ lấy hàng";

                db.Orders.Add(order);
                db.SaveChanges(); // để có OrderID

                foreach (var item in cart)
                {
                    if (item != null)
                    {
                        var product = db.Products.Find(item.ProductID);

                        if (product != null && product.Quantity >= item.Quantity)
                        {
                            var detail = new OrderDetail();
                            detail.OrderID = order.OrderID;
                            detail.ProductID = item.ProductID;
                            detail.Quantity = item.Quantity;
                            detail.UnitPrice = item.UnitPrice;
                            db.OrderDetails.Add(detail);

                            product.Quantity = product.Quantity - item.Quantity;
                            product.SoldQuantity = product.SoldQuantity + item.Quantity;
                        }
                        else
                        {
                            throw new Exception($"Sản phẩm {item.ProductName} không đủ số lượng tồn kho (Hiện còn: {product?.Quantity ?? 0}).");
                        }
                    }
                }

                db.SaveChanges();

                Session["Cart"] = null;
                TempData["OrderSuccessMessage"] = "Đặt hàng thành công!";

                return RedirectToAction("OrderHistory", "Default");
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Lỗi đặt hàng: " + ex.Message;

                ViewBag.Cart = cart;
                ViewBag.Customer = db.Customers.Find(GetCurrentCustomerID());
                return View("Checkout");
            }
        }

        public ActionResult OrderConfirmation()
        {
            if (TempData["OrderSuccessMessage"] == null)
            {
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
    }
}
