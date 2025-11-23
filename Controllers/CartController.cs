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

       
        // (Đây là hàm nội bộ để lấy giỏ hàng ra)
        private List<CartItem> GetCart()
        {
            // Kiểm tra xem Session Cart có tồn tại không
            List<CartItem> cart = Session["Cart"] as List<CartItem>;

            // Nếu chưa tồn tại (lần đầu vào), tạo mới 1 list rỗng
            if (cart == null)
            {
                cart = new List<CartItem>();
                Session["Cart"] = cart; // Lưu list rỗng vào Session
            }
            return cart;
        }

        private void SaveCart(List<CartItem> cart)
        {
            Session["Cart"] = cart;
        }

        
        // GET: /Cart/Index
        public ActionResult Index()
        {
            List<CartItem> cart = GetCart(); // Lấy giỏ hàng

            // Nếu giỏ hàng rỗng, gửi thông báo
            if (cart.Count == 0)
            {
                ViewBag.CartMessage = "Giỏ hàng của bạn đang trống.";
            }

            return View(cart); // Gửi danh sách item đến View
        }


        // (Action này được gọi từ trang Details)
        // POST: /Cart/AddToCart
        [HttpPost]
        public ActionResult AddToCart(int productID, int quantity)
        {
            List<CartItem> cart = GetCart();
            CartItem item = cart.Where(i => i != null).FirstOrDefault(i => i.ProductID == productID);

            var productDB = db.Products.Find(productID);

            // 
            int requiredQuantity = quantity;
            if (item != null)
            {
                // Nếu đã có trong giỏ, tính tổng số lượng sẽ có sau khi thêm
                requiredQuantity = item.Quantity + quantity;
            }

            if (productDB == null || productDB.Quantity < requiredQuantity)
            {
                // Nếu sản phẩm không tồn tại hoặc không đủ số lượng
                TempData["Error"] = $"Sản phẩm {productDB?.ProductName ?? "này"} chỉ còn {productDB?.Quantity ?? 0} sản phẩm. Vui lòng giảm số lượng.";
                return RedirectToAction("Index"); // Quay lại trang giỏ hàng với thông báo lỗi
            }
            // 


            if (item != null)
            {
                // Nếu có, chỉ tăng số lượng
                item.Quantity += quantity;
            }
            else
            {
                // Nếu chưa có, tạo item mới
                if (productDB != null)
                {
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
            }
            SaveCart(cart);
            return RedirectToAction("Index");
        }



        // GET: /Cart/RemoveFromCart/5
        public ActionResult RemoveFromCart(int productID)
        {
            List<CartItem> cart = GetCart();
            CartItem item = cart.Where(i => i != null).FirstOrDefault(i => i.ProductID == productID);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }

        // (Hàm dọn dẹp DbContext)
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
            return 0; // Trả về 0 nếu không tìm thấy (ví dụ: Admin đăng nhập)
        }

        
        [Authorize(Roles = "C")] // Chỉ ai có vai trò "C" (Customer) mới được vào
        public ActionResult Checkout()
        {
            List<CartItem> cart = GetCart();
            if (cart.Count == 0 || cart.Any(i => i == null))
            {
                // Nếu giỏ hàng rỗng (hoặc hỏng), đá về trang giỏ hàng
                return RedirectToAction("Index");
            }

            // Lấy thông tin khách hàng hiện tại
            int customerID = GetCurrentCustomerID();
            var customer = db.Customers.Find(customerID);

            // Gửi thông tin giỏ hàng và khách hàng đến View Checkout
            ViewBag.Cart = cart;
            ViewBag.Customer = customer;

            return View(); // Mở trang Checkout.cshtml
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
                // TẠO ĐƠN HÀNG
                var order = new Order();
                order.CustomerID = GetCurrentCustomerID();
                order.OrderDate = DateTime.Now;
                order.TotalAmount = TotalAmount;
                order.AddressDelivery = AddressDelivery;

                // 
                // Dùng PaymentMethod để set trạng thái PaymentStatus
                order.PaymentStatus = (PaymentMethod == "Tiền mặt (COD)") ? "Chờ xử lý" : "Chờ xác nhận chuyển khoảng";

                // 
                order.ShippingStatus = "Chờ lấy hàng";

                db.Orders.Add(order);
                db.SaveChanges(); // Lưu Order để lấy OrderID

                // TẠO CHI TIẾT & TRỪ KHO
                foreach (var item in cart)
                {
                    if (item != null)
                    {
                        var product = db.Products.Find(item.ProductID);

                        // kt ban neu con du hang tronh kho
                        if (product != null && product.Quantity >= item.Quantity)
                        {
                            var detail = new OrderDetail();
                            detail.OrderID = order.OrderID;
                            detail.ProductID = item.ProductID;
                            detail.Quantity = item.Quantity;
                            detail.UnitPrice = item.UnitPrice;
                            db.OrderDetails.Add(detail);

                            // cap nhat nhat cap nhat  so luong tu kho
                            product.Quantity = product.Quantity - item.Quantity;
                            product.SoldQuantity = product.SoldQuantity + item.Quantity;

                        }
                        else
                        {
                            // Ném lỗi để rollback (nếu có transaction) hoặc nhảy xuống catch
                            throw new Exception($"Sản phẩm {item.ProductName} không đủ số lượng tồn kho (Hiện còn: {product?.Quantity ?? 0}).");
                        }
                    }
                }

                db.SaveChanges(); // Lưu chi tiết và cập nhật kho

                Session["Cart"] = null;
                TempData["OrderSuccessMessage"] = "Đặt hàng thành công!";

                // Chuyển hướng đến trang Lịch sử mua hàng
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
        // trang tuy chon
        public ActionResult OrderConfirmation()
        {
            // Lấy thông báo từ TempData
            if (TempData["OrderSuccessMessage"] == null)
            {
                // Nếu người dùng truy cập trực tiếp mà không có thông báo, chuyển hướng về trang chủ
                return RedirectToAction("Index", "Default");
            }
            return View();
        }
        ///

    }
}