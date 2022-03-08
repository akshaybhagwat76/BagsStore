using BagsStore.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class StoreController : BaseController
    {
        private bags_Entities db = new bags_Entities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            int objects = Convert.ToInt16(form["objects"]);
            String name = form["name"];
            String street = form["street"];
            Double storePrice = Convert.ToDouble(form["price"]);
            bool partner = Convert.ToBoolean(form["partner"]);
            String link = form["link"];
            String emailConf = form["emailConf"];
            String mapPicture = form["mapPicture"];
            String whours = form["whours"];
            String promo = form["promo"];
            String realName = form["realName"];
            String additional = form["additional"];

            ViewBag.lastNameF = "";
            ViewBag.nameF = "";
            ViewBag.emailF = "";
            ViewBag.phoneF = "";

            if (!String.IsNullOrEmpty(form["nameF"]))
            {
                ViewBag.nameF = form["nameF"];
            }

            if (!String.IsNullOrEmpty(form["nameF"]))
            {
                ViewBag.lastNameF = form["lastNameF"];
            }

            if (!String.IsNullOrEmpty(form["nameF"]))
            {
                ViewBag.emailF = form["emailF"];
            }

            if (!String.IsNullOrEmpty(form["nameF"]))
            {
                ViewBag.phoneF = form["phoneF"];
            }

            
            
           

            ViewBag.promo = false;
            ViewBag.promoTry = true;

            if (!String.IsNullOrEmpty(promo))
            {
                switch(promo)
                {
                    case "FREEBAG":
                        storePrice = 0;
                        ViewBag.promo = true;
                        break;
                    case "10OFFBAG":
                        storePrice = Math.Round(storePrice - (storePrice * 0.10),2);
                        ViewBag.promo = true;
                        break;
                    case "15OFFBAG":
                        storePrice = Math.Round(storePrice - (storePrice * 0.15), 2);
                        ViewBag.promo = true;
                        break;
                    case "20OFFBAG":
                        storePrice = Math.Round(storePrice - (storePrice * 0.20), 2);
                        ViewBag.promo = true;
                        break;
                    default:
                        ViewBag.promoTry = false;
                        ViewBag.promo = false;
                        break;
                }
               
            }

            DateTime dropOffDate = Convert.ToDateTime(form["dropOff"]);
            DateTime pickUpDate = Convert.ToDateTime(form["pickUp"]);

            double days = (pickUpDate - dropOffDate).TotalDays;
            //days = Math.Round(days, 0);
            days = Math.Ceiling(days);

            storePrice *= objects;

            double price = storePrice * days;

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            ViewBag.name = name;
            ViewBag.price = price;
            ViewBag.street = street;
            ViewBag.partner = partner;
            ViewBag.link = link;
            ViewBag.emailConf = emailConf;
            ViewBag.mapPicture = mapPicture;
            ViewBag.whours = whours;
            ViewBag.realName = realName;
            ViewBag.additional = additional;

            StripeSettings stripe = new StripeSettings();
            stripe.SecretKey = WebConfigurationManager.AppSettings["StripeSecretApiKey"];
            stripe.PublishableKey = WebConfigurationManager.AppSettings["StripePublishApiKey"];

            return View(stripe);
        }

        [HttpPost]
        public ActionResult Book(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];
            String name = form["name"];
            String price = form["price"];

            String firstName = form["firstName"];
            String lastName = form["lastName"];
            String email = form["email"];
            String phone = form["phone"];

            String emailConf = form["emailConf"];
            String mapPicture = form["mapPicture"];
            String link = form["link"];
            String whours = form["whours"];
            String street = form["street"];
            String realName = form["realName"];
            String additional = form["additional"];

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
            res.PaymentMethod = "Store";
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
            model.PaymentMethod = res.PaymentMethod;
            model.MapPicture = mapPicture;
            model.Link = link;
            model.EmailConf = emailConf;
            model.WHours = whours;
            model.Street = street;
            model.Additional = additional;

            //  return new Rotativa.ViewAsPdf("GeneratePDF", model) { FileName = "Booking - " + (res.Id + 100) + ".pdf" };

            var pdfResult = new Rotativa.ViewAsPdf("GeneratePDF", model) { FileName = "Booking - " + (res.Id + 100) + ".pdf" };
             var binary = pdfResult.BuildPdf(this.ControllerContext);
             MemoryStream memoryStream = new MemoryStream(binary);
             Attachment attachment = new Attachment(memoryStream, "Booking - " + (res.Id + 100) + ".pdf", MediaTypeNames.Application.Pdf);

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


            String to2 = email;
            String subject2 = "Reservation";
            String msg2 = "<p>Thank you for booking!</p>" +
                "<p>If you have any further questions contact us on info@bagtostore or +386 51 847 142</p>";

            SendEmailFromOffice(to2, subject2, msg2, attachment);

            return RedirectToAction("Index", "Successful", new { id = res.Id });
        }

        [HttpPost]
        public ActionResult StripePayment(FormCollection form)
        {

            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];
            String name = form["name"];
            Double price = Convert.ToDouble(form["price"]);

            String firstName = form["firstName"];
            String lastName = form["lastName"];
            String phone = form["phone"];

            String email = form["stripeEmail"];
            String stripeToken = form["stripeToken"];

            String emailConf = form["emailConf"];
            String mapPicture = form["mapPicture"];
            String link = form["link"];
            String whours = form["whours"];
            String street = form["street"];
            String realName = form["realName"];
            String additional = form["additional"];

            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = email,
                Source = stripeToken
            });

            Double stripePrice = price * 100;

            var charge = charges.Create(new ChargeCreateOptions
            {
                Amount = (long)stripePrice,
                Description = objects + " bags",
                Currency = "eur",
                CustomerId = customer.Id,
            });
            //var options = new PaymentIntentCreateOptions
            //{
            //    Amount = (long)stripePrice,
            //    Description = objects + " bags",
            //    Currency = "eur",
            //    CustomerId = customer.Id,
            //    PaymentMethodTypes = new List<string>
            //      {
            //        "card",
            //      },
            //    PaymentMethodOptions= new PaymentIntentPaymentMethodOptionsOptions()

            //};
            //var service = new PaymentIntentService();
            //service.Create(options);

            if (charge.Status.Equals("succeeded"))
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
                res.Price = price.ToString();
                res.PaymentMethod = "Stripe";
                res.EmailConf = emailConf;
                res.Street = street;
                res.WHours = whours;
                res.Link = link;
                res.Additional = additional;
                res.MapPicture = mapPicture;

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
                model.Price = price.ToString();
                model.StoreName = realName;
                model.PaymentMethod = res.PaymentMethod;
                model.MapPicture = mapPicture;
                model.Link = link;
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

                return RedirectToAction("Stripe", "Successful", new { id = res.Id });
            }
            else
            {
                return RedirectToAction("Stripe", "Unsuccessful");
            }
            //return RedirectToAction("Stripe", "Unsuccessful");
        }
    }
}