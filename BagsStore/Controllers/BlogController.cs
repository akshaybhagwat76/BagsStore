using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BagsStore.Models;
using PagedList;

namespace BagsStore.Controllers
{
    public class BlogController : BaseController
    {
        private bags_Entities db = new bags_Entities();
        public ActionResult Index(int? page)
        {
            var items = from i in db.Items
                        join cat in db.Categories on i.Category_Id equals cat.Id
                        join cont in db.ItemContents on i.ItemContentId equals cont.Id
                        where cat.Id == 8
                        orderby i.CreatedDate descending
                        select new ViewItem
                        {
                            Id = cont.Id,
                            ItemId = i.Id,
                            Title = cont.Title,
                            Cat = cat.Name,
                            DateCreated = i.CreatedDate,
                            CreatedBy = i.CreatedBy,
                            Active = i.IsDeleted,
                            Image = cont.BigImage
                        };

            var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();
        }

        public ActionResult Post(int? id)
        {
            ItemContent item = db.ItemContents.Find(id);

            var items = (from i in db.Items
                        join cat in db.Categories on i.Category_Id equals cat.Id
                        join cont in db.ItemContents on i.ItemContentId equals cont.Id
                        where cat.Id == 8 && cont.Id != id
                        orderby i.CreatedDate descending
                        select new ViewItem
                        {
                            Id = cont.Id,
                            ItemId = i.Id,
                            Title = cont.Title,
                            Cat = cat.Name,
                            DateCreated = i.CreatedDate,
                            CreatedBy = i.CreatedBy,
                            Active = i.IsDeleted,
                            Image = cont.BigImage
                        }).Take(4);

            ViewBag.items = items;

            return View(item);
        }
    }
}