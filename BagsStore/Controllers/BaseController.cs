using BagsStore.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class BaseController : Controller
    {
        private bags_Entities db2 = new bags_Entities();

        [ValidateInput(false)]
        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {

            if (HttpContext.Request.Cookies["cookiee"] != null)
            {
                ViewBag.cookie = true;
            }
            else
            {
                ViewBag.cookie = false;
            }

            var locsPop = from l in db2.Locations
                       where l.Active == true
                       orderby l.Name
                       select l.Name;

            ViewBag.locsPop = locsPop;

            //--- Rishma added this code 01/03/2022 ---
            var locsPopUp = from l in db2.Locations
                          where l.Active == true
                          select l;

            ViewBag.locsPopUp = locsPopUp;

            return base.BeginExecuteCore(callback, state);
        }
        public static void SendEmailFromOffice(String to, String subject, String message, Attachment attachment)
        {
            SmtpSection secObj = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            SmtpClient smtp = new SmtpClient();
            smtp.Host = secObj.Network.Host;
            smtp.EnableSsl = secObj.Network.EnableSsl;
            NetworkCredential NetworkCred = new NetworkCredential(secObj.Network.UserName, secObj.Network.Password);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate,  X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };

            MailMessage mail = new MailMessage();

            if (attachment != null)
            {
                mail.Attachments.Add(attachment);
            }

            mail.From = new MailAddress(secObj.Network.UserName);
            mail.To.Add(to);
            mail.IsBodyHtml = true;
            mail.Subject = subject;
            mail.Body = message;
            smtp.Send(mail);
        }

    }
}