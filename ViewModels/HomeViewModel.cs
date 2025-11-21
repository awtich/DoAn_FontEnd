using DoAn_web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAn_web.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Iphones { get; set; }

        public List<Product> Macs { get; set; }
        public List<Product> Watches { get; set; }
        public List<Product> Ipads { get; set; }
        public List<Product> PhuKiens { get; set; }
        public HomeViewModel()
        {
            Iphones = new List<Product>();
            Macs = new List<Product>();
            Watches = new List<Product>();
            Ipads = new List<Product>();
            PhuKiens = new List<Product>();
        }
    }
}