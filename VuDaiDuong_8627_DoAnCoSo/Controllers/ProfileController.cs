using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Controllers
{

    public class ProfileController : Controller
    {
        DACSEntities db = new DACSEntities();
        public ActionResult Index()
        {

            if (Session["IdUser"] == null)
            {

                return RedirectToAction("Login", "Home");
            }


            int userId = (int)Session["IdUser"];


            var profile = db.Users.FirstOrDefault(u => u.IdUser == userId);

            if (profile == null)
            {

                return HttpNotFound();
            }

            return View(profile);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = db.Users.Where(n => n.IdUser == id).FirstOrDefault();
            ViewBag.IdRole = new SelectList(db.Roles, "IdRole", "NameRole", user.IdRole);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}