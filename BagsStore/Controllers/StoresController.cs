using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class StoresController : BaseController
    {
        private bags_Entities db = new bags_Entities();
        public ActionResult Index()
        {
            ViewBag.stores = from s in db.Stores
                             select s;

            Location loc = db.Locations.Find(1);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;

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
            String objects = form["objects"];

            if(!where.Equals("My Location") && !where.Equals("Ljubljana"))
            {
                return RedirectToAction("Index", "Home", new { id = 1 });
            }

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            ViewBag.stores = from s in db.Stores
                             select s;

            Location loc = db.Locations.Find(1);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;

            return View();
        }
    }
}