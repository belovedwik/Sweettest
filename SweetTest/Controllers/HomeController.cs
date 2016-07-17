using SweetTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace SweetTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            OrderItem model = new OrderItem();
            if (User.Identity.IsAuthenticated)
            {
                string Email = string.Empty;
                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    Email = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name).Email;
                }
                model.UserMail = Email;
                model.UserName = User.Identity.Name;
            }
          

            return View(model);
        }

        public ActionResult AddOrder(OrderItem model)
        {
            if (ModelState.IsValid)
            {
                model.OrderDate = DateTime.Now;
                model.ItemsCount = 1;

                using (OrderModels db = new OrderModels())
                {
                    db.Orders.Add(model);
                    db.SaveChanges(); 
                }

                try
                {
                    SendMail("smtp.gmail.com", "mymail@gmail.com", "myPassword", model.UserMail, "Заказ миньона", "Миньон заказан");
                }
                catch{
                    // to do...
                }
                
                return RedirectToAction("OrderList");
            }

            return View("Index", model);
        }

        public ActionResult OrderList()
        {
    
            List<OrderItem> orders;
            using (OrderModels db = new OrderModels())
            {
                orders = db.Orders.ToList();
            }

            return View(orders);
        }

        public static void SendMail(string smtpServer, string from, string password, string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));
                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }
       
    }
}