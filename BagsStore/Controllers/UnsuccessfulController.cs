using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class UnsuccessfulController : BaseController
    {
        // GET: Unsuccessful
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Stripe()
        {
            return View();
        }
    }
}