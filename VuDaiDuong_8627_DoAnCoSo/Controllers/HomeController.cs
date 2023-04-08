using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Controllers
{
    public class HomeController : Controller
    {
        DACSEntities db = new DACSEntities();
        public ActionResult Index(int page = 1, int pagesize = 3)
        {
            return View(db.Products.ToList().ToPagedList(page, pagesize));
        }
        public ActionResult DetailPro(int id)
        {
            Product pro = db.Products.SingleOrDefault(n => n.IdProduct == id);
            return View(pro);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}