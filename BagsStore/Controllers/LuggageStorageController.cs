using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{

    public class LuggageStorageController : BaseController
    {
        private bags_Entities db = new bags_Entities();
        public ActionResult Ljubljana()
        {
            ViewBag.stores = from s in db.Stores
                             where s.Location == 1
                             select s;

            Location loc = db.Locations.Find(1);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.lng = loc.Longitude;
            ViewBag.lat = loc.Latitude;
            ViewBag.where = loc.Name;
            ViewBag.dropOff = null;
            ViewBag.pickUp = null;
            ViewBag.objects = null;
            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                              where r.CityId == loc.Id
                              orderby r.Date descending
                              select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        public ActionResult Bled()
        {
            ViewBag.stores = from s in db.Stores
                             where s.Location == 2
                             select s;

            Location loc = db.Locations.Find(2);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.lng = loc.Longitude;
            ViewBag.lat = loc.Latitude;
            ViewBag.where = loc.Name;
            ViewBag.dropOff = null;
            ViewBag.pickUp = null;
            ViewBag.objects = null;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        public ActionResult Belgrade()
        {
            ViewBag.stores = from s in db.Stores
                             where s.Location == 1002
                             select s;

            Location loc = db.Locations.Find(1002);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.lng = loc.Longitude;
            ViewBag.lat = loc.Latitude;
            ViewBag.where = loc.Name;
            ViewBag.dropOff = null;
            ViewBag.pickUp = null;
            ViewBag.objects = null;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        public ActionResult Nis()
        {
            ViewBag.stores = from s in db.Stores
                             where s.Location == 1003
                             select s;

            Location loc = db.Locations.Find(1003);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.lng = loc.Longitude;
            ViewBag.lat = loc.Latitude;
            ViewBag.where = loc.Name;
            ViewBag.dropOff = null;
            ViewBag.pickUp = null;
            ViewBag.objects = null;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        public ActionResult Vienna()
        {
            var stores = from s in db.Stores
                             where s.Location == 1004
                             select s;

            ViewBag.stores = stores;

            Location loc = db.Locations.Find(1004);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.lng = loc.Longitude;
            ViewBag.lat = loc.Latitude;
            ViewBag.where = loc.Name;
            ViewBag.dropOff = null;
            ViewBag.pickUp = null;
            ViewBag.objects = null;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        public ActionResult Budapest()
        {
            var stores = from s in db.Stores
                         where s.Location == 1006
                         select s;

            ViewBag.stores = stores;

            Location loc = db.Locations.Find(1006);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.lng = loc.Longitude;
            ViewBag.lat = loc.Latitude;
            ViewBag.where = loc.Name;
            ViewBag.dropOff = null;
            ViewBag.pickUp = null;
            ViewBag.objects = null;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        [HttpPost]
        public ActionResult Ljubljana(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];

            if(dropOff.Equals(pickUp))
            {
                ViewBag.equalDates = true;
            }

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            ViewBag.stores = from s in db.Stores
                             where s.Location == 1
                             select s;

            Location loc = db.Locations.Find(1);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.goToLuggage = true;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        [HttpPost]
        public ActionResult Bled(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];

            if (dropOff.Equals(pickUp))
            {
                ViewBag.equalDates = true;
            }

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            ViewBag.stores = from s in db.Stores
                             where s.Location == 2
                             select s;

            Location loc = db.Locations.Find(2);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.goToLuggage = true;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        [HttpPost]
        public ActionResult Belgrade(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];

            if (dropOff.Equals(pickUp))
            {
                ViewBag.equalDates = true;
            }

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            ViewBag.stores = from s in db.Stores
                             where s.Location == 1002
                             select s;

            Location loc = db.Locations.Find(1002);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.goToLuggage = true;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        [HttpPost]
        public ActionResult Nis(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            ViewBag.stores = from s in db.Stores
                             where s.Location == 1003
                             select s;

            Location loc = db.Locations.Find(1003);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.goToLuggage = true;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        [HttpPost]
        public ActionResult Vienna(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];

            if (dropOff.Equals(pickUp))
            {
                ViewBag.equalDates = true;
            }

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            ViewBag.stores = from s in db.Stores
                             where s.Location == 1004
                             select s;

            Location loc = db.Locations.Find(1004);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.goToLuggage = true;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        [HttpPost]
        public ActionResult Budapest(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];

            if (dropOff.Equals(pickUp))
            {
                ViewBag.equalDates = true;
            }

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            ViewBag.stores = from s in db.Stores
                             where s.Location == 1006
                             select s;

            Location loc = db.Locations.Find(1006);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.goToLuggage = true;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        [HttpPost]
        public ActionResult Search(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];

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

            var locs = from l in db.Locations select l;
            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        public ActionResult Search()
        {
            ViewBag.stores = from s in db.Stores
                             select s;

            Location loc = db.Locations.Find(1);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.lng = loc.Longitude;
            ViewBag.lat = loc.Latitude;
            ViewBag.where = loc.Name;
            ViewBag.dropOff = null;
            ViewBag.pickUp = null;
            ViewBag.objects = null;

            var locs = from l in db.Locations select l;
            ViewBag.locs = locs;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        public ActionResult Store(int? id)
        {
            ViewBag.stores = from s in db.Stores
                             where s.Location == id
                             select s;

            Location loc = db.Locations.Find(id);

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.lng = loc.Longitude;
            ViewBag.lat = loc.Latitude;
            ViewBag.where = loc.Name;
            ViewBag.LocationId = loc.Id;
            ViewBag.dropOff = null;
            ViewBag.pickUp = null;
            ViewBag.objects = null;
            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var locate = from c in db.Locations
                         where c.Active == true
                         select c;
            ViewBag.locate = locate;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        [HttpPost]
        public ActionResult Store(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];
            int id = Convert.ToInt32(form["city"]);
            if (dropOff.Equals(pickUp))
            {
                ViewBag.equalDates = true;
            }

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            
            ViewBag.stores = from s in db.Stores
                             where s.Location == id
                             select s;

            Location loc = db.Locations.Find(id);
            ViewBag.LocationId = loc.Id;
            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.goToLuggage = true;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var locate = from c in db.Locations
                         where c.Active == true
                         select c;
            ViewBag.locate = locate;

            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        public ActionResult In(string cityname)
        {
            Location loc = db.Locations.Where(x=>x.Name==cityname).FirstOrDefault();
            ViewBag.stores = from s in db.Stores
                             where s.Location == loc.Id
                             select s;

            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.lng = loc.Longitude;
            ViewBag.lat = loc.Latitude;
            ViewBag.where = loc.Name;
            ViewBag.LocationId = loc.Id;
            ViewBag.dropOff = null;
            ViewBag.pickUp = null;
            ViewBag.objects = null;
            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var locate = from c in db.Locations
                         where c.Active == true
                         select c;
            ViewBag.locate = locate;
            var desc = db.Locations.Where(x => x.Id == loc.Id).Select(y => y.Description).FirstOrDefault();
            ViewBag.Description = desc;
            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }

        [HttpPost]
        public ActionResult In(FormCollection form)
        {
            String lng = form["lng"];
            String lat = form["lat"];
            String where = form["where"];
            String dropOff = form["dropOff"];
            String pickUp = form["pickUp"];
            String objects = form["objects"];
            //int id = Convert.ToInt32(form["city"]);
            if (dropOff.Equals(pickUp))
            {
                ViewBag.equalDates = true;
            }

            ViewBag.lng = lng;
            ViewBag.lat = lat;
            ViewBag.where = where;
            ViewBag.dropOff = dropOff;
            ViewBag.pickUp = pickUp;
            ViewBag.objects = objects;
            
            Location loc = db.Locations.Where(x => x.Name == where).FirstOrDefault();
            ViewBag.stores = from s in db.Stores
                             where s.Location == loc.Id
                             select s;

            
            ViewBag.LocationId = loc.Id;
            ViewBag.latLj = loc.Latitude;
            ViewBag.lngLj = loc.Longitude;
            ViewBag.goToLuggage = true;

            var locs = from l in db.Locations
                       where l.Active == true
                       select l.Name;

            ViewBag.locs = locs;

            var locate = from c in db.Locations
                         where c.Active == true
                         select c;
            ViewBag.locate = locate;
            var desc = db.Locations.Where(x => x.Id == loc.Id).Select(y => y.Description).FirstOrDefault();
            ViewBag.Description = desc;
            var reviews = from r in db.Reviews
                          where r.CityId == loc.Id
                          orderby r.Date descending
                          select r;

            ViewBag.haveReviews = false;

            if (reviews.Any())
            {
                ViewBag.haveReviews = true;
            }

            ViewBag.reviews = reviews;

            return View();
        }
    }
}