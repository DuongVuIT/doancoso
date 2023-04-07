using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        DACSEntities db = new DACSEntities();
        public ActionResult Categories()
        {
            return PartialView(db.Categories.ToList());
        }
        public ViewResult ProductsCategories(int id, int page = 1, int Pagesize = 6)
        {

            List<Product> sanpham = db.Products.Where(n => n.IdCategory == id).ToList();

            return View(sanpham.ToList().ToPagedList(page, Pagesize));
        }
    }
}