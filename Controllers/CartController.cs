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
            List<CartItem> cart = GetCart(); // Lấy giỏ hàng

            // 1. Kiểm tra xem item đã có trong giỏ chưa
            // 
            CartItem item = cart.Where(i => i != null).FirstOrDefault(i => i.ProductID == productID);

            if (item != null)
            {
                // Nếu có, chỉ tăng số lượng
                item.Quantity += quantity;
            }
            else
            {
                // Nếu chưa có, lấy thông tin sản phẩm từ DB
                var product = db.Products.Find(productID);
                if (product != null)
                {
                    // Tạo một CartItem mới (dùng Model bạn vừa tạo)
                    var newItem = new CartItem
                    {
                        ProductID = product.ProductID,
                        ProductName = product.ProductName,
                        ProductImage = product.ProductImage,
                        Quantity = quantity,
                        UnitPrice = product.ProductPrice // Dùng ProductPrice từ CSDL
                    };
                    cart.Add(newItem);
                }
            }

            SaveCart(cart); // Lưu lại vào Session

            // Quay lại trang giỏ hàng
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
        [Authorize(Roles = "C")] // Khóa lại 1 lần nữa cho chắc
        [ValidateAntiForgeryToken]
        public ActionResult PlaceOrder(string AddressDelivery, decimal TotalAmount)
        {
            // 1. Lấy giỏ hàng từ Session
            List<CartItem> cart = GetCart();
            if (cart.Count == 0 || cart.Any(i => i == null))
            {
                return RedirectToAction("Index"); // Giỏ hàng rỗng, quay về
            }

            try
            {
                // 2. Tạo Đơn hàng (Lưu vào bảng [Order])
                var order = new Order();
                order.CustomerID = GetCurrentCustomerID();
                order.OrderDate = DateTime.Now; // Lấy ngày giờ hiện tại
                order.TotalAmount = TotalAmount;
                order.AddressDelivery = AddressDelivery;
                order.PaymentStatus = "Chưa thanh toán"; // Mặc định

                db.Orders.Add(order);
                db.SaveChanges(); // <-- LƯU LẦN 1 (Để lấy được OrderID)

                // 3. Tạo Chi tiết Đơn hàng (Lưu vào bảng [OrderDetail])
                foreach (var item in cart)
                {
                    if (item != null)
                    {
                        var detail = new OrderDetail();
                        detail.OrderID = order.OrderID; // <-- Lấy OrderID của đơn hàng vừa tạo
                        detail.ProductID = item.ProductID;
                        detail.Quantity = item.Quantity;
                        detail.UnitPrice = item.UnitPrice;

                        db.OrderDetails.Add(detail);
                    }
                }

                db.SaveChanges(); // 

                //  Xóa giỏ hàng (vì đã đặt thành công)
                Session["Cart"] = null;

                //  Chuyển đến trang Cảm ơn hoặc Lịch sử mua hàng
                // (Chúng ta sẽ làm trang này ở Phần 4)
                return RedirectToAction("OrderHistory", "Default");
            }
            catch (Exception ex)
            {
                // Nếu có lỗi, quay lại trang Checkout và báo lỗi
                // (Bạn nên log 'ex' lại để debug)
                ViewBag.Error = "Đã xảy ra lỗi khi đặt hàng. Vui lòng thử lại.";

                // Cần gửi lại ViewBag cho trang Checkout
                ViewBag.Cart = cart;
                ViewBag.Customer = db.Customers.Find(GetCurrentCustomerID());
                return View("Checkout");
            }
        }

    }
}