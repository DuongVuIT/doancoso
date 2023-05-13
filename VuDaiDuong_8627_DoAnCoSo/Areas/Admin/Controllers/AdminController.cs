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
            
            ViewBag.visitor = HttpContext.Application["visitor"].ToString();// lay so luong nguoi truy cap tu application
            ViewBag.sales = Sales();
            ViewBag.user = CountUser();
            ViewBag.orders = Orders();
            if (Session["IdUser"] == null || Session["IdRole"] == null || (int)Session["IdRole"] != 1)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }


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
                    Session["FullName"] = data.FirstOrDefault().FullName;

                    Session["IdUser"] = data.FirstOrDefault().IdUser;
                    Session["IdRole"] = data.FirstOrDefault().IdRole;
                    if (Session["IdRole"] != null && int.Parse(Session["IdRole"].ToString()) == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Admin");
                    }
                }
                else
                {
                    ViewBag.error = "Login Fail";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
        public double Sales()
        {
            double sales = db.Orders.Sum(n=>n.Total);
            ViewBag.salesFormatted = string.Format("{0:#,##0} VNĐ", sales);

            return sales;
        }
        public int CountUser()
        {
            int user = db.Users.Count();
            return user;
        }

        public int Orders()
        {
            int orders = db.Orders.Count();
            return orders;
        }
    }
}