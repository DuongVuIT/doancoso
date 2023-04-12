using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.UI;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Controllers
{
    public class HomeController : Controller
    {
        DACSEntities db = new DACSEntities();
        public ActionResult Index(int page = 1, int pagesize = 3)
        {
            return View(db.Products.ToList().ToPagedList(page, pagesize));
        }
        public ActionResult DetailPro(int id)
        {
            Product pro = db.Products.SingleOrDefault(n => n.IdProduct == id);
            return View(pro);

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                var checkmail = db.Users.FirstOrDefault(n => n.Email == user.Email);
                var checkusername = db.Users.FirstOrDefault(n => n.UserName == user.UserName);
                if (checkmail == null && checkusername == null)
                {
                    user.IdRole = 2; 
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("RegisterSuccess");
                }
                else if (checkusername != null)
                {
                    ViewBag.username = "Username already exists.";
                }
                else
                {
                    ViewBag.email = "Email already exists.";
                }
            }
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
                var tk = db.Users.Where(n => n.UserName.Equals(username));
                var mk = db.Users.Where(n => n.PassWord.Equals(password));
                if (tk.Count() > 0 && mk.Count() > 0)
                {
                    Session["FullName"] = tk.FirstOrDefault().FullName;
                    Session["UserName"] = tk.FirstOrDefault().UserName;
                    Session["Email"] = tk.FirstOrDefault().Email;
                    Session["Phone"] = tk.FirstOrDefault().Phone;
                    Session["Address"] = tk.FirstOrDefault().Address;
                    Session["Role"] = tk.FirstOrDefault().Role;
                    Session["IdUser"] = tk.FirstOrDefault().IdUser;
                    Session["IdRole"] = tk.FirstOrDefault().IdRole;
                    if (Session["IdRole"] != null && int.Parse(Session["IdRole"].ToString()) == 1)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }


                }
                else if (tk.Count() > 0 || mk.Count() < 0)
                {
                    ViewBag.errormk = "Password incorrect";
                    return View();
                }
                else if (tk.Count() < 0 || mk.Count() > 0)
                {
                    ViewBag.errortk = "Username incorrect";
                    return View();
                }
                else
                {
                    ViewBag.errortk = "Username incorrect";
                    ViewBag.errormk = "Password incorrect";
                    return View();
                }

            }
            return View();


        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }
        public ActionResult RegisterSuccess()
        {
            return View();
        }
    }
}