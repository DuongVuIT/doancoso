using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Controllers
{
    public class CategoriesController : Controller
    {
        // GET: Categories
        DACSEntities db = new DACSEntities();
        public ActionResult Categories()
        {
            return PartialView(db.Categories.ToList());
        }
        public ViewResult ProductsCategories(int id, int page = 1, int Pagesize = 6)
        {

            List<Product> sanpham = db.Products.Where(n => n.IdCategory == id).ToList();

            return View(sanpham.ToList().ToPagedList(page, Pagesize));
        }
        public ActionResult AddToCart(int id, int quantity)
        {
            if (Session["cart"] == null)
            {
                List<Cart> cart = new List<Cart>();
                cart.Add(new Cart { Product = db.Products.Find(id), Quantity = quantity });
                Session["cart"] = cart;
                Session["count"] = 1;
            }
            else
            {
                List<Cart> cart = (List<Cart>)Session["cart"];

                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity += quantity;
                }
                else
                {
                    cart.Add(new Cart { Product = db.Products.Find(id), Quantity = quantity });
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;

                }
                Session["cart"] = cart;
            }
            return Json(new { Message = "Thành Công", ItemCount = Session["count"], JsonRequestBehavior.AllowGet });
        }

        private int isExist(int id)
        {
            List<Cart> cart = (List<Cart>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Product.IdProduct.Equals(id))
                    return i;
            }
            return -1;
        }
    }
}