using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        DACSEntities db = new DACSEntities();
        public ActionResult Index()
        {
            var user = db.Users.ToList();
            if (Session["IdUser"] == null || Session["IdRole"] == null || (int)Session["IdRole"] != 1)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View(user);
            }
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var user = db.Users.Where(n => n.IdUser == id).FirstOrDefault();
            return View(user);
        }

    }
}