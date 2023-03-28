using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        DACSEntities db = new DACSEntities();
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
       
        [ValidateAntiForgeryToken]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {

                var data = db.Users.Where(n => n.UserName.Equals(username) && n.PassWord.Equals(password));
                if (data.Count() > 0)
                {
                    Session["name"] = data.FirstOrDefault().FullName;
                    Session["role"] = data.FirstOrDefault().Role;
                    Session["id"] = data.FirstOrDefault().IdUser;
                    if (Session["role"] == null)
                    {
                        return Redirect("~");
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //ViewBag.error = "Login Fail";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
    }
}