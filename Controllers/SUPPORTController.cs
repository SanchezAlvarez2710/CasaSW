using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CasaSW.Controllers
{
    public class SUPPORTController : Controller
    {
        // GET: SUPPORT
        // GET: EmailConfig
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(CasaSW.Models.Mail model)
        {
            MailMessage mm = new MailMessage(model.From,"pruebascs123@outlook.com");
            mm.Subject = model.Subject;
            mm.Body = model.Body;
            mm.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.Host = "smtp.office365.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("pruebascs123@outlook.com", "constru1");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = nc;
            smtp.Send(mm);
            ViewBag.Message = "mail has been sent succesfully.";
            return View();
        }
    }
}
