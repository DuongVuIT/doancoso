using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;

namespace VuDaiDuong_8627_DoAnCoSo.Controllers
{
    public class CheckoutController : Controller
    {
        DACSEntities db = new DACSEntities();
        // GET: Checkout
        [HttpGet]
        public ActionResult Checkout()
        {
            return View((List<Cart>)Session["cart"]);
        }
        [HttpPost]
        public ActionResult Checkout(string Name, string Phone, string Email, string Address, string Des)
        {
            int IdUser = (int)Session["IdUser"];
            if (IdUser == 0)
            {
                return RedirectToAction("Login", "Home");
                
            }
            List<Cart> ck = (List<Cart>)Session["cart"];

            Order order = new Order();
            order.Date = DateTime.UtcNow.AddHours(7);
            order.IdUser = IdUser;
            order.Name = Name;
            order.Email = Email;
            order.Address = Address;
            order.Phone = Phone;
            order.Des = Des;
            order.Total = ck.Sum(n => n.Quantity * n.Product.Price);

            Session["Total"] = order.Total;
            Session["FullName"] = order.Name;
            Session["Phone"] = order.Phone;
            Session["Des"] = order.Des;
            Session["Address"] = order.Address;
            Session["Date"] = order.Date;

            db.Orders.Add(order);
            db.SaveChanges();

            int details = order.IdOrder;
            List<OrderDetail> orderDetails = new List<OrderDetail>();

            foreach (var item in ck)
            {
                OrderDetail od = new OrderDetail();
                od.IdOrder = details;
                od.ProductName = item.Product.Name;
                od.IdProduct = item.Product.IdProduct;
                od.Quantity = item.Quantity;
                od.Price = item.Product.Price;
                od.Total = item.Quantity * item.Product.Price;

                db.OrderDetails.Add(od);
                db.SaveChanges();
            }

            return RedirectToAction("CheckoutSuccess");
        }


        public ActionResult CheckoutSuccess()
        {
            return View();
        }
    }
}