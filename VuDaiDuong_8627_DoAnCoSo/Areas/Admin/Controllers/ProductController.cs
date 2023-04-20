using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        DACSEntities db = new DACSEntities();
        // GET: Admin/Products
        public ActionResult Index()
        {
            var pro = db.Products.ToList();
            if (Session["IdUser"] == null || Session["IdRole"] == null || (int)Session["IdRole"] != 1)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(pro);
            }

        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "CategoryName");
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product sp, HttpPostedFileBase ImageUpload)
        {
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName = fileName + extension;
                sp.Image = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images"), fileName));
            }
            db.Products.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var product = db.Products.Where(n => n.IdProduct == id).FirstOrDefault();
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {

            var product = db.Products.Where(n => n.IdProduct == id).FirstOrDefault();
            return View(product);
        }
        [HttpPost]

        public ActionResult Delete(int id)
        {
            var product = db.Products.Where(n => n.IdProduct == id).FirstOrDefault();
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var pro = db.Products.Where(n=>n.IdProduct == id).FirstOrDefault();
            ViewBag.IdCategory = new SelectList(db.Categories, "IdCategory", "CategoryName", pro.IdCategory);
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(Product pro, HttpPostedFileBase ImageUpload)
        {
            if (ImageUpload != null && ImageUpload.ContentLength > 0)
            {
                string fileName = Path.GetFileNameWithoutExtension(ImageUpload.FileName);
                string extension = Path.GetExtension(ImageUpload.FileName);
                fileName = fileName + extension;
                pro.Image = fileName;
                ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images"), fileName));
            }

            db.Entry(pro).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}