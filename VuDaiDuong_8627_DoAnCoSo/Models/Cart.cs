using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Models
{

    public class Cart
    {
        DACSEntities db = new DACSEntities();
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public double Total { get; set; }

    }
}