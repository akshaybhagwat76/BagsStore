using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class BecomeOurPartnerController : BaseController
    {
        private bags_Entities db = new bags_Entities();
        public ActionResult Index()
        {
            ItemContent item = db.ItemContents.Find(4);
            return View(item);
        }
    }
}