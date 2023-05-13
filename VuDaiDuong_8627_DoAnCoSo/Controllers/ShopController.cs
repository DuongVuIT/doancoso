using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Controllers
{
    public class ShopController : Controller
    {
        DACSEntities db = new DACSEntities();
        public ActionResult Index(int page = 1, int pagesize = 6)
        {
            return View(db.Products.ToList().ToPagedList(page, pagesize));
           
        }
        public ActionResult DetailPro(int id)
        {
            Product pro = db.Products.SingleOrDefault(n => n.IdProduct == id);
            return View(pro);
            
        }
       
    }
}