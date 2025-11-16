using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAn_web.Models;
using DoAn_web.ViewModels;
using System.Data.Entity;

namespace DoAn_web.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        //Con CHó kiệt
         private MyStore2026Entities db = new MyStore2026Entities();

        public ActionResult Index()
        {
           // tao cai hop cho ViewModel
           var viewModel = new HomeViewModel();

            // lay 4 san pham iphone ra
            viewModel.Iphones=db.Products
                .Include(p=>p.Category)
                .Where(p=>p.Category.CategoryName=="Iphone")// loc
                .OrderByDescending(p=>p.ProductID)// lay sp moi nhat
                .Take(4)// lay 4 sp
                .ToList();

            // 4 sp ipad
                        viewModel.Ipads = db.Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName == "Ipad")// loc
                .OrderByDescending(p => p.ProductID)// lay sp moi nhat
                .Take(4)// lay 4 sp
                .ToList();
            // 4 sp mac
                        viewModel.Macs = db.Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName == "Mac")// loc
                .OrderByDescending(p => p.ProductID)// lay sp moi nhat
                .Take(4)// lay 4 sp
                    .ToList();
            // 4 sp watch
                        viewModel.Watches = db.Products
                .Include(p => p.Category)
                .Where(p => p.Category.CategoryName == "Watch")// loc
                .OrderByDescending(p => p.ProductID)// lay sp moi nhat
                .Take(4)// lay 4 sp
                .ToList();
            //6 gui du lieu ve View
            return View(viewModel);




        }
        public ActionResult Cart()
        {
            return View();
        }
        public ActionResult UserLogin()
        {
            return View();
        }

        public ActionResult TrangIphone()
        {
            //  Lọc database:
            // CHỈ lấy các sản phẩm có CategoryName == "iPhone"
            var iphones = db.Products
                            .Include(p => p.Category)
                            .Where(p => p.Category.CategoryName == "iPhone")
                            .OrderByDescending(p => p.ProductID) // Lấy sp mới nhất
                            .ToList();

            //  Gửi danh sách ĐÃ LỌC này đến View
            return View(iphones);
        }
        public ActionResult TrangMac()
        {
            return View();
        }
        public ActionResult TrangWatch()
        {
            return View();
        }
        public ActionResult TrangIpad()
        {
            return View();
        }
        public ActionResult TrangMussic()
        {
            return View();
        }
        public ActionResult TrangCamera()
        {
            return View();
        }
        public ActionResult TrangAccessory()
        {
            return View();
        }
        public ActionResult PhuKien()
        {
            return View();
        }
        public ActionResult AmThanh()
        {
            return View();
        }
        public ActionResult Camera()
        {
            return View();
        }
        public ActionResult GiaDung()
        {
            return View();
        }
        public ActionResult MayLuot()
        {
            return View();
        }
        public ActionResult TinTuc()
        {
            return View();
        }
        public ActionResult DichVu()
        {
            return View();
        }
        public ActionResult UserSighUp()
        {
            return View();
        }
        public ActionResult Address()
        {
            return View();
        }
        public ActionResult Confirm()
        {
            return View();
        }
        public ActionResult OrderHistory()
        {
            return View();
        }
    }
}