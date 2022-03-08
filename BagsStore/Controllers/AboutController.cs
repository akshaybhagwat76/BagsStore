using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class AboutController : BaseController
    {
        private bags_Entities db = new bags_Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            var faq = from i in db.Items
                      join cat in db.Categories on i.Category_Id equals cat.Id
                      join cont in db.ItemContents on i.ItemContentId equals cont.Id
                      where cat.Id == 3
                      select new ViewItem
                      {
                          Id = cont.Id,
                          ItemId = i.Id,
                          Title = cont.Title,
                          SecondTitle = cont.SecondTitle
                       };

            ViewBag.faq = faq;

            return View();
        }

        public ActionResult Partner()
        {
            ItemContent item = db.ItemContents.Find(4);
            return View(item);
        }

        public ActionResult HowItWorks()
        {
            ItemContent item = db.ItemContents.Find(3);
            return View(item);
        }

        public ActionResult WhoWeAre()
        {
            ItemContent item = db.ItemContents.Find(8);
            return View(item);
        }

        public ActionResult Terms()
        {
            ItemContent item = db.ItemContents.Find(7);
            return View(item);
        }

        public ActionResult Privacy()
        {
            ItemContent item = db.ItemContents.Find(6);
            return View(item);
        }
    }
}