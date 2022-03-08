using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class whoweareController : BaseController
    {
        private bags_Entities db = new bags_Entities();
        // GET: whoweare
        public ActionResult Index()
        {
            ItemContent item = db.ItemContents.Find(8);
            return View(item);
        }
    }
}