using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using VuDaiDuong_8627_DoAnCoSo.Models;
using System.Net.Mail;
using System.Net;

namespace VuDaiDuong_8627_DoAnCoSo.Controllers
{
    public class CheckoutController : Controller
    {
        DACSEntities db = new DACSEntities();
        [HttpGet]
        public ActionResult Checkout()
        {
            return View((List<Cart>)Session["cart"]);
        }
        [HttpPost]
        public ActionResult Checkout(string Name, string Phone, string Email, string Address, string Des)
        {

           
            List<Cart> ck = (List<Cart>)Session["cart"];

            Ordered order = new Ordered();
            order.Date = DateTime.UtcNow.AddHours(7);

            order.Name = Name;
            order.Email = Email;
            order.Address = Address;
            order.Phone = Phone;
            order.Des = Des;
            order.Total = ck.Sum(n => n.Quantity * n.Product.Price);
            if (Session["IdUser"] != null)
            {

                order.IdUser = int.Parse(Session["IdUser"].ToString());
            }
            else
            {
                
                return RedirectToAction("Login", "Home");
            }

            Session["Total"] = order.Total;
            Session["Name"] = order.Name;
            Session["Phone"] = order.Phone;
            Session["Des"] = order.Des;
            Session["Email"] = order.Email;
            Session["Address"] = order.Address;
            Session["Date"] = order.Date;
            db.Ordereds.Add(order);
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
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("duongvuit.26@gmail.com");
            mail.To.Add(new MailAddress(Email));
            mail.Subject = "Your order has been placed successfully";
            mail.Body = "Thank you for placing your order with us. Your order details:\n\n" +
                        "Họ tên : " + order.Name + "\n" +
                        "Số điện thoại : " + order.Phone + "\n" +
                        "Email : " + order.Email + "\n" +
                        "Địa chỉ : " + order.Address + "\n" +
                        "Tổng tiền : " + order.Total.ToString("#,##0.00") + " VNĐ\n\n" +
                        "We will contact you soon to confirm the details of your order.";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.Credentials = new NetworkCredential("duongvuit.26@gmail.com", "qoaxmvlrbqxzquhl");
            smtp.EnableSsl = true;
            smtp.Send(mail);
           
            return RedirectToAction("CheckoutSuccess");
            
        }


        public ActionResult CheckoutSuccess()
        {
            
            return View();

        }
    }
}