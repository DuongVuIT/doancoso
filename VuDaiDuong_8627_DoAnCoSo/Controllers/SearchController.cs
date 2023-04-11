using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Controllers
{
    public class SearchController : Controller
    {
        DACSEntities db = new DACSEntities();
        [HttpGet]
        public ActionResult Search(String keyword, int page = 1, int Pagesize = 10)
        {
            ViewBag.TuKhoa = keyword;
            List<Product> timkiem = db.Products.Where(n => n.Name.Contains(keyword)).ToList();
            if (timkiem.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.Products.OrderBy(n => n.Name).ToPagedList(page, Pagesize));
            }
            else
            {
                ViewBag.ThongBao = "Đã Tìm Thấy " + timkiem.Count + " Kết quả";
                return View(timkiem.OrderBy(n => n.Name).ToPagedList(page, Pagesize));
            }
           
        }
        [HttpPost]
        public ActionResult Search(FormCollection f, int page = 1, int Pagesize = 10)
        {
            string keyword = f["txtTimkiem"].ToString();
            ViewBag.TuKhoa = keyword;
            List<Product> timkiem = db.Products.Where(n => n.Name.Contains(keyword)).ToList();
            if (timkiem.Count == 0)
            {
                ViewBag.ThongBao = "Không tìm thấy sản phẩm nào";
                return View(db.Products.OrderBy(n => n.Name).ToPagedList(page, Pagesize));
            }
            else
            {
                ViewBag.ThongBao = "Đã Tìm Thấy " + timkiem.Count + " Kết quả";
                return View(timkiem.OrderBy(n => n.Name).ToPagedList(page, Pagesize));
            }
        }

    }
}