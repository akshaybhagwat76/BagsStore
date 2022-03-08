using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BagsStore.Controllers
{
    public class PostController : BaseController
    {
        private bags_Entities db = new bags_Entities();
        public ActionResult Index(String t)
        {
            t = t.Replace('-', ' ');
            ItemContent item = db.ItemContents.Where(x => x.Title.Equals(t)).FirstOrDefault();

            var items = (from i in db.Items
                         join cat in db.Categories on i.Category_Id equals cat.Id
                         join cont in db.ItemContents on i.ItemContentId equals cont.Id
                         where cat.Id == 8 && cont.Id != item.Id
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