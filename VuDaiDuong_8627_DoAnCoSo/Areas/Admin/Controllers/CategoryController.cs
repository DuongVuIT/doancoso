using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        DACSEntities db = new DACSEntities();
        public ActionResult Index()
        {
            var cate = db.Categories.ToList();
            if (Session["IdUser"] == null)
            {
                return RedirectToAction("Login","Admin");
            }
            else
            {
                return View(cate);
            }
            
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Category cate)
        {
            db.Categories.Add(cate);
            db.SaveChanges();
            return RedirectToAction("index");
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var cate = db.Categories.Where(n => n.IdCategory == id).FirstOrDefault();
            return View(cate);
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var cate = db.Categories.Where(n => n.IdCategory == id).FirstOrDefault();
            return View(cate);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var cate = db.Categories.Where(n => n.IdCategory == id).FirstOrDefault();
            db.Categories.Remove(cate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var cate = db.Categories.Where(n => n.IdCategory == id).FirstOrDefault();
            return View(cate);
        }
        [HttpPost]
        public ActionResult Edit(Category cate)
        {
            db.Entry(cate).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }

}