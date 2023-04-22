using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        DACSEntities db = new DACSEntities();
      
        public ActionResult Index()
        {
            var order = db.Orders.ToList();
            return View(order);
        }
        public ActionResult Details(int id)
        {
            var order = db.OrderDetails.Where(n => n.IdOrder == id).FirstOrDefault();
            return View(order);
        }
        [HttpGet]
        public ActionResult Edit(int ?id)
        {
            var order = db.Orders.Where(n => n.IdOrder == id).FirstOrDefault();
            ViewBag.IdUser = order.Name.ToString();
            return View(order);
        }
        [HttpPost]
        public ActionResult Edit(Order order)
        {
            db.Entry(order).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}