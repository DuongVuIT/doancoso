using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Parser.SyntaxTree;
using System.Web.UI.WebControls;
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
            List<User> users = db.Users.Where(n => n.IdUser == id).ToList();

            return View(users.ToList());
           
        }
        [HttpGet]
        public ActionResult Delete(int ?id)
        {
            var user = db.Users.Where(n => n.IdUser == id).FirstOrDefault();
            return View(user);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var iduser = db.Users.Where(n => n.IdUser == id).FirstOrDefault();
            db.Users.Remove(iduser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        

    }
}

