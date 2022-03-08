using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class HomeController : BaseController
    {
        private bags_Entities db = new bags_Entities();

        public ActionResult Index(int? id)
        {
            if(id == 1)
            {
                ViewBag.pop = true;
            }

            Location loc = db.Locations.Find(1);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;

            Picture p = db.Pictures.Find(1);
            ViewBag.p = p;

            var testimonials = from t in db.Testimonials
                               select t;

            var locs = from l in db.Locations 
                       where l.Active == true
                       select l.Name;
            var locate = from c in db.Locations
                       where c.Active == true
                       select c;
            ViewBag.locs = locs;
            ViewBag.locate = locate;
            ViewBag.testimonials = testimonials;
            ViewBag.testn = testimonials.Count();
            
            return View();
        }


        [HttpPost]
        public ActionResult Cookie()
        {

            HttpCookie cookie = new HttpCookie("cookiee");
            cookie.Value = "cookie";
            cookie.Expires = DateTime.Now.AddDays(365);
            HttpContext.Response.SetCookie(cookie);

            return Json(true);
        }

        [HttpPost]
        public ActionResult GetLoc(string place)
        {

            var pl = db.Locations.Where(x => x.Name.Equals(place)).FirstOrDefault();

            if (pl == null)
            {
                return Json(false);
            }
            else
            {
                string[] cords = { pl.Latitude, pl.Longitude };

                return Json(cords);
            }


        }


    }
}