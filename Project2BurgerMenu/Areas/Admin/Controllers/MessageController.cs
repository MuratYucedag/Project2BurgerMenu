using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project2BurgerMenu.Context;
using Project2BurgerMenu.Entities;

namespace Project2BurgerMenu.Areas.Admin.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        BurgerMenuContext context = new BurgerMenuContext();
        public ActionResult InBox()
        {
            var userName = Session["x"];
            var email = context.Admins.Where(x => x.Username == userName).Select(y => y.Email).FirstOrDefault();
            var values = context.Messages.Where(x => x.ReceiverEmail == email).ToList();
            return View(values);
        }
        public ActionResult SendBox()
        {
            var userName = Session["x"];
            var email = context.Admins.Where(x => x.Username == userName).Select(y => y.Email).FirstOrDefault();
            var values = context.Messages.Where(x => x.SenderEmail == email).ToList();
            return View(values);
        }
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Project2BurgerMenu.Entities.Message message)
        {
            message.IsRead = false;
            message.SendDate = DateTime.Now;
            context.Messages.Add(message);
            context.SaveChanges();
            return RedirectToAction("Sendbox", "Message", new { area = "Admin" });
        }
    }
}