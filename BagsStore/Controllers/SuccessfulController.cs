using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class SuccessfulController : BaseController
    {
        private bags_Entities db = new bags_Entities();
        // GET: Successful
        public ActionResult Index(int? id)
        {
            Reservation res = db.Reservations.Find(id);
            return View(res);
        }

        public ActionResult Stripe(int? id)
        {
            Reservation res = db.Reservations.Find(id);
            return View(res);
        }

          public ActionResult Paypal(String lng, String lat, String where, String dropOff, String pickUp, String objects, String name, String price, String link, String mapPicture, String emailConf, String whours, String street, String realName, String additional, String firstName, String lastName, String phone, String email)
          {

            Reservation res = new Reservation();

            res.Bags = objects;
            res.Date = DateTime.Now;
            res.DropOff = dropOff;
            res.Email = email;
            res.Latitude = lat;
            res.Longitude = lng;
            res.Name = firstName;
            res.Phone = phone;
            res.PickUp = pickUp;
            res.StoreName = realName;
            res.Surname = lastName;
            res.Price = price;
            res.PaymentMethod = "Paypal";
            res.EmailConf = emailConf;
            res.Street = street;
            res.WHours = whours;
            res.Link = link;
            res.MapPicture = mapPicture;
            res.Additional = additional;

            db.Reservations.Add(res);
            db.SaveChanges();

            var model = new GeneratePDFModel();

            model.Bags = objects;
            model.DropOff = dropOff;
            model.Email = email;
            model.LastName = lastName;
            model.Name = firstName;
            model.Number = (res.Id + 100).ToString();
            model.Phone = phone;
            model.PickUp = pickUp;
            model.Price = price;
            model.StoreName = realName;
            model.Link = link;
            model.MapPicture = mapPicture;
            model.EmailConf = emailConf;
            model.WHours = whours;
            model.Street = street;
            model.Additional = additional;

            //  return new Rotativa.ViewAsPdf("GeneratePDF", model) { FileName = "Booking - " + (res.Id + 100) + ".pdf" };

            var pdfResult = new Rotativa.ViewAsPdf("GeneratePDF", model) { FileName = "Booking - " + (res.Id + 100) + ".pdf" };
            var binary = pdfResult.BuildPdf(this.ControllerContext);
            MemoryStream memoryStream = new MemoryStream(binary);
            Attachment attachment = new Attachment(memoryStream, "Booking - " + (res.Id + 100) + ".pdf", MediaTypeNames.Application.Pdf);

            if (emailConf.Equals("info@bagtostore.com"))
            {
                String to = emailConf;
                String subject = "Reservation";
                String msg = "<p>Name: " + firstName + "</p>" +
                    "<p>Surname: " + lastName + "</p>" +
                    "<p>Email: " + email + "</p>" +
                    "<p>Phone: " + phone + "</p>" +
                    "<p>Location: " + name + "</p>" +
                    "<p>Bags: " + objects + "</p>" +
                    "<p>Drop off: " + dropOff + "</p>" +
                    "<p>Pick up: " + pickUp + "</p>";

                SendEmailFromOffice(to, subject, msg, attachment);
            }
            else
            {
                String to = emailConf;
                String subject = "Reservation";
                String msg = "<p>Name: " + firstName + "</p>" +
                    "<p>Surname: " + lastName + "</p>" +
                    "<p>Email: " + email + "</p>" +
                    "<p>Phone: " + phone + "</p>" +
                    "<p>Location: " + name + "</p>" +
                    "<p>Bags: " + objects + "</p>" +
                    "<p>Drop off: " + dropOff + "</p>" +
                    "<p>Pick up: " + pickUp + "</p>";

                SendEmailFromOffice(to, subject, msg, attachment);

                String to3 = "info@bagtostore.com";

                SendEmailFromOffice(to3, subject, msg, attachment);
            }


            String to2 = email;
            String subject2 = "Reservation";
            String msg2 = "<p>Thank you for booking!</p>" +
                "<p>If you have any further questions contact us on info@bagtostore or +386 51 847 142</p>";

            SendEmailFromOffice(to2, subject2, msg2, attachment);

            return View(res);
          }

     
    }
}