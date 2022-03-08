using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class FAQController : BaseController
    {
        private bags_Entities db = new bags_Entities();
        public ActionResult Index()
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
    }
}