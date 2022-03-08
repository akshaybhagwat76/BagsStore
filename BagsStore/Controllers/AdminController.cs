using BagsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Net;
using System.IO;
using BagsStore.ViewModel;

namespace BagsStore.Controllers
{
    public class AdminController : Controller
    {
        private bags_Entities db = new bags_Entities();

        public ActionResult Login()
        {
            if (HttpContext.Request.Cookies["admin"] != null)
            {
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        public ActionResult CheckCredentials(FormCollection form)
        {

            string user = form["user"];
            string pass = form["pass"];
            var admin = db.Admins.FirstOrDefault(u => u.Username.Equals(user) && u.Password.Equals(pass));

            if (admin == null)
            {

                return RedirectToAction("Index", new { id = 1 });
            }
            else
            {
                HttpCookie cookie = new HttpCookie("admin");
                cookie.Value = user;
                cookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.SetCookie(cookie);
                return RedirectToAction("Index");

            }

        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            var c = new HttpCookie("admin");
            c.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(c);
            return RedirectToAction("Login", "Admin");
        }


        public ActionResult Search(String title, String category, int? page)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                if (category == "all")
                {
                    var items = from i in db.Items
                                join cat in db.Categories on i.Category_Id equals cat.Id
                                join cont in db.ItemContents on i.ItemContentId equals cont.Id
                                orderby i.CreatedDate descending
                                where cont.Title.Contains(title)
                                select new ViewItem
                                {
                                    Id = cont.Id,
                                    ItemId = i.Id,
                                    Title = cont.Title,
                                    Cat = cat.Name,
                                    DateCreated = i.CreatedDate,
                                    CreatedBy = i.CreatedBy,
                                    Active = i.IsDeleted
                                };

                    var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                    var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                    var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                    ViewBag.OnePageOfProducts = onePageOfProducts;
                }
                else
                {
                    int catId = Convert.ToInt32(category);

                    var items = from i in db.Items
                                join cat in db.Categories on i.Category_Id equals cat.Id
                                join cont in db.ItemContents on i.ItemContentId equals cont.Id
                                orderby i.CreatedDate descending
                                where cont.Title.Contains(title) && i.Category_Id == catId
                                select new ViewItem
                                {
                                    Id = cont.Id,
                                    ItemId = i.Id,
                                    Title = cont.Title,
                                    Cat = cat.Name,
                                    DateCreated = i.CreatedDate,
                                    CreatedBy = i.CreatedBy,
                                    Active = i.IsDeleted
                                };

                    var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                    var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                    var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                    ViewBag.OnePageOfProducts = onePageOfProducts;
                }



                var categories = from c in db.Categories
                                 select c;


                ViewBag.categories = categories;

                ViewBag.cat = category;
                ViewBag.query = title;

                return View();
            }
        }

        public ActionResult SearchGallery(String title, int? page)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {


                var items = from i in db.GaleryItems
                            where i.Name.Contains(title)
                            orderby i.CreatedDate descending
                            select i;

                var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;


                ViewBag.query = title;

                return View();
            }
        }

      

        public ActionResult Upload(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string url; // url to return
            string message; // message to display (optional)
            message = "Image successfully saved ";

            //do save image code here
            url = Server.MapPath("~/upload") + "/" + upload.FileName;
            upload.SaveAs(url);



            string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\", \"" + message + "\");</script></body></html>";

            return JavaScript("Successfully sent");
        }

        //Vesti
        // GET: /Admin/
        public ActionResult Index(int? page)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                var items = from r in db.Reservations
                            orderby r.Date descending
                            select r;

                var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;

                return View();
            }
        }

        // GET: /Admin/Delete/5
        public ActionResult View(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Reservation itemContent = db.Reservations.Find(id);
                return View(itemContent);
            }
        }

        
        public ActionResult Activate(int? id)
        {
            var item = db.Items.Find(id);

            item.IsDeleted = false;

            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Deactivate(int? id)
        {
            var item = db.Items.Find(id);

            item.IsDeleted = true;

            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }





        //Gallery
        // GET: /Admin/
        public ActionResult Gallery(int? page)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                var items = from g in db.GaleryItems
                            orderby g.CreatedDate descending
                            select g;

                var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;

                return View();
            }
        }

        //GET: /Admin/Create
        public ActionResult CreateGallery()
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                var categories = from c in db.Categories
                                 select c;

                var gallery = from g in db.GaleryItems
                              orderby g.CreatedDate descending
                              select g;

                ViewBag.gallery = gallery;
                ViewBag.categories = categories;

                return View();
            }
        }

        // POST: /Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateGallery(FormCollection form, HttpPostedFileBase Picture)
        {

            GaleryItem galleryItem = new GaleryItem();

            galleryItem.Name = form["Name"];
            galleryItem.CreatedDate = DateTime.Now;
            galleryItem.CreatedBy = "admin";
            galleryItem.IsDeleted = true;


            db.GaleryItems.Add(galleryItem);
            db.SaveChanges();


            string path = Server.MapPath("~/Images/" + form["Name"]);

            System.IO.Directory.CreateDirectory(path);



            return RedirectToAction("Gallery");

        }

        // GET: /Admin/Edit/5
        public ActionResult EditGallery(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                GaleryItem galleryItem = db.GaleryItems.Find(id);

                var images = from i in db.GaleryImages
                             where i.GaleryItem_Id == galleryItem.Id
                             select i;

                if (galleryItem == null)
                {
                    return HttpNotFound();
                }

                ViewBag.item = galleryItem;

                ViewBag.images = images;


                return View();
            }
        }


        public ActionResult DeleteImage(int? id)
        {
            GaleryImage g = db.GaleryImages.Find(id);
            int gId = g.GaleryItem_Id;

            db.GaleryImages.Remove(g);
            db.SaveChanges();

            return RedirectToAction("EditGallery", "Admin", new { id = gId });


        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditGallery(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            GaleryItem galleryItem = db.GaleryItems.Find(id);

            galleryItem.Name = form["Name"];
            galleryItem.CreatedDate = Convert.ToDateTime(form["dateInsert"]);
            galleryItem.ModifiedDate = DateTime.Now;
            galleryItem.CreatedBy = form["admin"];
            galleryItem.IsDeleted = Convert.ToBoolean(form["act"]);


            if (Picture != null)
            {

                GaleryImage galleryImage = new GaleryImage();

                string path = Path.Combine(Server.MapPath("~/Images/" + form["Name"]),
                                           Path.GetFileName(Picture.FileName));
                Picture.SaveAs(path);
                ViewBag.Message = "File uploaded successfully";
                galleryImage.BigImage = Picture.FileName;
                galleryImage.SmallImage = Picture.FileName;
                galleryImage.CreatedDate = DateTime.Now;
                galleryImage.CreatedBy = "admin";
                galleryImage.GaleryItem_Id = id;
                galleryImage.ImageDescription = form["imgDesc"];

                db.GaleryImages.Add(galleryImage);

            }


            db.Entry(galleryItem).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Gallery");
        }

        // GET: /Admin/Delete/5
        public ActionResult DeleteGallery(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                GaleryItem galleryItem = db.GaleryItems.Find(id);

                if (galleryItem == null)
                {
                    return HttpNotFound();
                }

                ViewBag.item = galleryItem;



                return View();
            }
        }

        // POST: /Admin/Delete/5
        [HttpPost, ActionName("DeleteGallery")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmedGallery(FormCollection form)
        {

            int id = Convert.ToInt32(form["id"]);


            GaleryItem galleryItem = db.GaleryItems.Find(id);


            var images = from i in db.GaleryImages
                         where i.GaleryItem_Id == galleryItem.Id
                         select i;

            foreach (GaleryImage g in images)
            {
                db.GaleryImages.Remove(g);
            }

            db.SaveChanges();

            db.GaleryItems.Remove(galleryItem);

            db.SaveChanges();

            return RedirectToAction("Gallery");
        }


        public ActionResult ActivateGallery(int? id)
        {
            var item = db.GaleryItems.Find(id);

            item.IsDeleted = false;

            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Gallery");
        }

        public ActionResult DeactivateGallery(int? id)
        {
            var item = db.GaleryItems.Find(id);

            item.IsDeleted = true;

            db.Entry(item).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Gallery");
        }




        public ActionResult EditPicture(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                Picture itemContent = db.Pictures.Find(1);
               

                if (itemContent == null)
                {
                    return HttpNotFound();
                }

                ViewBag.itemContent = itemContent;

                return View();
            }
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPicture(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            Picture itemContent = db.Pictures.Find(id);

           
            if (Picture != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"),
                                           Path.GetFileName(Picture.FileName));
                Picture.SaveAs(path);
                ViewBag.Message = "File uploaded successfully";
                itemContent.Picture1 = Picture.FileName;
            }

            db.Entry(itemContent).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult EditHowItWorks(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                
                ItemContent itemContent = db.ItemContents.Find(3);
               
                ViewBag.itemContent = itemContent;

              
                return View();
            }
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditHowItWorks(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            ItemContent itemContent = db.ItemContents.Find(id);

            itemContent.Title = form["title"];
            itemContent.Content = form["longTxt"];

            if (Picture != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"),
                                           Path.GetFileName(Picture.FileName));
                Picture.SaveAs(path);
                ViewBag.Message = "File uploaded successfully";
                itemContent.BigImage = Picture.FileName;
                itemContent.SmallImage = Picture.FileName;



            }
          
            db.Entry(itemContent).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

          
            return RedirectToAction("Index");
        }

        public ActionResult EditBecomeOurPartner(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
              
                ItemContent itemContent = db.ItemContents.Find(4);

                ViewBag.itemContent = itemContent;


                return View();
            }
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBecomeOurPartner(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            ItemContent itemContent = db.ItemContents.Find(id);

            itemContent.Title = form["title"];
            itemContent.Content = form["longTxt"];

            if (Picture != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"),
                                           Path.GetFileName(Picture.FileName));
                Picture.SaveAs(path);
                ViewBag.Message = "File uploaded successfully";
                itemContent.BigImage = Picture.FileName;
                itemContent.SmallImage = Picture.FileName;



            }

            db.Entry(itemContent).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult EditPrivacyPolicy(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                
                ItemContent itemContent = db.ItemContents.Find(6);

                ViewBag.itemContent = itemContent;


                return View();
            }
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPrivacyPolicy(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            ItemContent itemContent = db.ItemContents.Find(id);

            itemContent.Title = form["title"];
            itemContent.Content = form["longTxt"];

            db.Entry(itemContent).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult EditTerms(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                
                ItemContent itemContent = db.ItemContents.Find(7);

                ViewBag.itemContent = itemContent;


                return View();
            }
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditTerms(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            ItemContent itemContent = db.ItemContents.Find(id);

            itemContent.Title = form["title"];
            itemContent.Content = form["longTxt"];

          
            db.Entry(itemContent).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Index");
        }

        public ActionResult EditWhoAreWe(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
              
                ItemContent itemContent = db.ItemContents.Find(8);

                ViewBag.itemContent = itemContent;


                return View();
            }
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditWhoAreWe(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            ItemContent itemContent = db.ItemContents.Find(id);

            itemContent.Title = form["title"];
            itemContent.Content = form["longTxt"];

            if (Picture != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"),
                                           Path.GetFileName(Picture.FileName));
                Picture.SaveAs(path);
                ViewBag.Message = "File uploaded successfully";
                itemContent.BigImage = Picture.FileName;
                itemContent.SmallImage = Picture.FileName;



            }

            db.Entry(itemContent).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Index");
        }


        public ActionResult FAQ(int? page)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                var items = from i in db.Items
                            join cat in db.Categories on i.Category_Id equals cat.Id
                            join cont in db.ItemContents on i.ItemContentId equals cont.Id
                            where cat.Id == 3
                            orderby i.CreatedDate descending
                            select new ViewItem
                            {
                                Id = cont.Id,
                                ItemId = i.Id,
                                Title = cont.Title,
                                Cat = cat.Name,
                                DateCreated = i.CreatedDate,
                                CreatedBy = i.CreatedBy,
                                Active = i.IsDeleted
                            };

                var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;

                var categories = from c in db.Categories
                                 select c;


                ViewBag.categories = categories;

                return View();
            }
        }

        //GET: /Admin/Create
        public ActionResult CreateFAQ()
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }
        }

        // POST: /Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateFAQ(FormCollection form, HttpPostedFileBase Picture)
        {

            ItemContent itemContent = new ItemContent();

            itemContent.Title = form["title"];
            itemContent.SecondTitle = form["secondTitle"];
            itemContent.SortDescription = "";
            itemContent.Content = "";
            itemContent.NumOfView = 0;
            DateTime date = DateTime.Now;
            itemContent.CreatedDate = date;
            itemContent.CreatedBy = "admin";
            itemContent.ImageDescription = "";
            itemContent.BigImage = "";
            itemContent.SmallImage = "";

            db.ItemContents.Add(itemContent);
            db.SaveChanges();

            Item item = new Item();

            item.Category_Id = 3;
            item.ItemContentId = itemContent.Id;
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = "admin";
            item.IsDeleted = true;

            db.Items.Add(item);
            db.SaveChanges();

            return RedirectToAction("FAQ");

        }

        // GET: /Admin/Edit/5
        public ActionResult EditFAQ(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ItemContent itemContent = db.ItemContents.Find(id);
                Item item = (from i in db.Items
                             where i.ItemContentId == id
                             select i).FirstOrDefault();

                if (itemContent == null)
                {
                    return HttpNotFound();
                }

                ViewBag.item = item;

                ViewBag.itemContent = itemContent;


                return View();
            }
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditFAQ(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            ItemContent itemContent = db.ItemContents.Find(id);

            itemContent.Title = form["title"];
            itemContent.SortDescription = form["shortTxt"];

            db.Entry(itemContent).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            
            return RedirectToAction("FAQ");
        }

        // GET: /Admin/Delete/5
        public ActionResult DeleteFAQ(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ItemContent itemContent = db.ItemContents.Find(id);
                Item item = (from i in db.Items
                             where i.ItemContentId == id
                             select i).FirstOrDefault();

                if (itemContent == null)
                {
                    return HttpNotFound();
                }

                ViewBag.item = item;

                ViewBag.itemContent = itemContent;

                return View();
            }
        }

        // POST: /Admin/Delete/5
        [HttpPost, ActionName("DeleteFAQ")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmedFAQ(FormCollection form)
        {

            int id = Convert.ToInt32(form["id"]);

            Item item = (from i in db.Items
                         where i.ItemContentId == id
                         select i).FirstOrDefault();

            ItemContent itemContent = db.ItemContents.Find(id);


            db.Items.Remove(item);
            db.SaveChanges();

            db.ItemContents.Remove(itemContent);
            db.SaveChanges();

            return RedirectToAction("FAQ");
        }

        public ActionResult DownloadPDF(int? id)
        {


            Reservation res = db.Reservations.Find(id);

            var model = new GeneratePDFModel();
            
            model.Bags = res.Bags;
            model.DropOff = res.DropOff;
            model.Email = res.Email;
            model.LastName = res.Surname;
            model.Name = res.Name;
            model.Number = (res.Id+100).ToString();
            model.Phone = res.Phone;
            model.PickUp = res.PickUp;
            model.Price = res.Price;
            model.StoreName = res.StoreName;
            model.PaymentMethod = res.PaymentMethod;
            model.EmailConf = res.EmailConf;
            model.MapPicture = res.MapPicture;
            model.WHours = res.WHours;
            model.Link = res.Link;
            model.Street = res.Street;
            model.Additional = res.Additional;

            return new Rotativa.ViewAsPdf("GeneratePDF", model) { FileName = "Booking - " + (res.Id + 100) + ".pdf" };
            return View();
        }

        [HttpPost]
        public ActionResult DeleteReservation(FormCollection form)
        {

            int id = Convert.ToInt32(form["id"]);

            Reservation res = db.Reservations.Find(id);


            db.Reservations.Remove(res);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Blog(int? page)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
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
                                Active = i.IsDeleted
                            };

                var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;

                var categories = from c in db.Categories
                                 select c;


                ViewBag.categories = categories;

                return View();
            }
        }

        //GET: /Admin/Create
        public ActionResult CreateBlog()
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                return View();
            }
        }

        // POST: /Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateBlog(FormCollection form, HttpPostedFileBase Picture)
        {

            ItemContent itemContent = new ItemContent();

            itemContent.Title = form["title"];
            itemContent.SecondTitle = "";
            itemContent.SortDescription = form["shortDesc"];
            itemContent.Content = form["Content"];
            itemContent.NumOfView = 0;
            DateTime date = DateTime.Now;
            itemContent.CreatedDate = date;
            itemContent.CreatedBy = "admin";
            itemContent.ImageDescription = "";

            if (Picture != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"),
                                           Path.GetFileName(Picture.FileName));
                Picture.SaveAs(path);
                ViewBag.Message = "File uploaded successfully";
                itemContent.BigImage = Picture.FileName;
                itemContent.SmallImage = Picture.FileName;
            }

            db.ItemContents.Add(itemContent);
            db.SaveChanges();

            Item item = new Item();

            item.Category_Id = 8;
            item.ItemContentId = itemContent.Id;
            item.CreatedDate = DateTime.Now;
            item.CreatedBy = "admin";
            item.IsDeleted = true;

            db.Items.Add(item);
            db.SaveChanges();

            return RedirectToAction("Blog");

        }

        // GET: /Admin/Edit/5
        public ActionResult EditBlog(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ItemContent itemContent = db.ItemContents.Find(id);
                Item item = (from i in db.Items
                             where i.ItemContentId == id
                             select i).FirstOrDefault();

                if (itemContent == null)
                {
                    return HttpNotFound();
                }

                ViewBag.item = item;

                ViewBag.itemContent = itemContent;


                return View();
            }
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditBlog(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            ItemContent itemContent = db.ItemContents.Find(id);

            itemContent.Title = form["title"];
            itemContent.SortDescription = form["shortDesc"];
            itemContent.Content = form["Content"];

            if (Picture != null)
            {
                string path = Path.Combine(Server.MapPath("~/Images/"),
                                           Path.GetFileName(Picture.FileName));
                Picture.SaveAs(path);
                ViewBag.Message = "File uploaded successfully";
                itemContent.BigImage = Picture.FileName;
                itemContent.SmallImage = Picture.FileName;
            }

            db.Entry(itemContent).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();


            return RedirectToAction("Blog");
        }

        // GET: /Admin/Delete/5
        public ActionResult DeleteBlog(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ItemContent itemContent = db.ItemContents.Find(id);
                Item item = (from i in db.Items
                             where i.ItemContentId == id
                             select i).FirstOrDefault();

                if (itemContent == null)
                {
                    return HttpNotFound();
                }

                ViewBag.item = item;

                ViewBag.itemContent = itemContent;

                return View();
            }
        }

        // POST: /Admin/Delete/5
        [HttpPost, ActionName("DeleteBlog")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmedBlog(FormCollection form)
        {

            int id = Convert.ToInt32(form["id"]);

            Item item = (from i in db.Items
                         where i.ItemContentId == id
                         select i).FirstOrDefault();

            ItemContent itemContent = db.ItemContents.Find(id);


            db.Items.Remove(item);
            db.SaveChanges();

            db.ItemContents.Remove(itemContent);
            db.SaveChanges();

            return RedirectToAction("Blog");
        }


        public ActionResult Reviews(int? page)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                var items = from r in db.Reviews
                            orderby r.Date descending
                            select r;

                var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;

                var categories = from c in db.Categories
                                 select c;


                ViewBag.categories = categories;

                return View();
            }
        }

        //GET: /Admin/Create
        public ActionResult CreateReview()
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.locations = from l in db.Locations
                                    select l;
                return View();
            }
        }

        // POST: /Admin/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateReview(FormCollection form, HttpPostedFileBase Picture)
        {

            Review review = new Review();

            review.Name = form["name"];
            review.LastName = form["lastName"];
            review.Date = DateTime.Now;
            review.Comment = form["comment"];
            review.CityId = Convert.ToInt32(form["cityId"]);
            review.Country = form["country"];
            review.Rating = 0;

            db.Reviews.Add(review);
            db.SaveChanges();

            return RedirectToAction("Reviews");

        }

        // GET: /Admin/Edit/5
        public ActionResult EditReview(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Review review = db.Reviews.Find(id);
                ViewBag.locations = from l in db.Locations
                                    select l;
                return View(review);
            }
        }

        // POST: /Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditReview(FormCollection form, HttpPostedFileBase Picture)
        {

            int id = Convert.ToInt32(form["id"]);

            Review review = db.Reviews.Find(id);

            review.Name = form["name"];
            review.LastName = form["lastName"];
            review.Date = DateTime.Now;
            review.Comment = form["comment"];
            review.CityId = Convert.ToInt32(form["cityId"]);
            review.Country = form["country"];

            db.Entry(review).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Reviews");
        }

        // GET: /Admin/Delete/5
        public ActionResult DeleteReview(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Review review = db.Reviews.Find(id);

                return View(review);
            }
        }

        // POST: /Admin/Delete/5
        [HttpPost, ActionName("DeleteReview")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmedReview(FormCollection form)
        {

            int id = Convert.ToInt32(form["id"]);


            Review review = db.Reviews.Find(id);

            db.Reviews.Remove(review);
            db.SaveChanges();

            return RedirectToAction("Reviews");
        }

        //GET: /Admin/CreateLocation
        public ActionResult CreateLocation()
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.locations = from l in db.Locations
                                    select l;
                return View();
            }
        }

        public ActionResult Locations(int? page)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {

                var items = from r in db.Locations
                            orderby r.Id descending
                            select r;

                var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;


                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateLocation(FormCollection form)
        {
            Location loc = new Location();
            loc.Latitude = form["Latitude"];
            loc.Longitude = form["Longitude"];
            loc.Name = form["Name"];
            loc.Description = form["Description"];
            loc.Active = Convert.ToBoolean(form["Active"]);
            db.Locations.Add(loc);
            db.SaveChanges();
            return RedirectToAction("Locations");

        }

        public ActionResult EditLocation(int? id)
        {

            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Location loc = db.Locations.Find(id);
                ViewBag.locations = from l in db.Locations
                                    select l;
                return View(loc);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditLocation(FormCollection form)
        {
            int id = Convert.ToInt32(form["id"]);

            Location loc = db.Locations.Find(id);
            loc.Latitude = form["Latitude"];
            loc.Longitude = form["Longitude"];
            loc.Name = form["Name"];
            loc.Description = form["Description"];
            loc.Active = Convert.ToBoolean(form["Active"]);

            db.Entry(loc).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Locations");
        }
       
        public ActionResult DeleteLocation(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Location loc = db.Locations.Find(id);

                return View(loc);
            }
        }

        [HttpPost, ActionName("DeleteLocation")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmedLocation(FormCollection form)
        {

            int id = Convert.ToInt32(form["id"]);

            Location loc = db.Locations.Find(id);

            db.Locations.Remove(loc);
            db.SaveChanges();

            return RedirectToAction("Locations");
        }
        public ActionResult Stores(int? page)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                //ShowStore_VM items = new ShowStore_VM();
               var items = (from r in db.Stores join l in db.Locations on r.Location equals l.Id
                            orderby r.Id descending
                            select new ShowStore_VM
                            {
                                Id=r.Id,
                                Name=r.Name,
                                Street=r.Street,
                                Latitude=r.Latitude,
                                Longitude=r.Longitude,
                                Type=r.Type,
                                WorkingHours=r.WorkingHours,
                                Price=r.Price,
                                Picture= "../Images/" + r.Picture,
                                Partner=r.Partner,
                                Location=l.Name,
                                Email=r.Email,
                                MapPicture=r.MapPicture,
                                Link=r.Link,
                                RealName=r.RealName,
                                HourFrom=r.HourFrom,
                                HourTo=r.HourTo,
                                Services=r.Services,
                                DayOff=r.DayOff,
                                Additional=r.Additional,
                                PictureMobile=r.PictureMobile
                            });

                var products = items; //returns IQueryable<Product> representing an unknown number of products. a thousand maybe?

                var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
                var onePageOfProducts = products.ToPagedList(pageNumber, 10); // will only contain 25 products max because of the pageSize

                ViewBag.OnePageOfProducts = onePageOfProducts;
               // var loc = from c in db.Locations select c;


               // ViewBag.locations = loc;

                return View();
            }
        }                
        public ActionResult CreateStore()
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                ViewBag.loc = from l in db.Locations
                                    select l;
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateStore(FormCollection form, HttpPostedFileBase Picture, HttpPostedFileBase MapPicture, HttpPostedFileBase PictureMobile)
        {
            Store st = new Store();
            st.Name = form["Name"];
            st.Street = form["Street"];
            st.Latitude = form["Latitude"];
            st.Longitude = form["Longitude"];
            st.Type = form["Type"];
            st.Latitude = form["Latitude"];
            st.WorkingHours = form["WorkingHours"];
            st.Price = form["Price"];
            
            if (form["Partner"] == "1")
            {
                st.Partner = true;
            }
            else
            {
                st.Partner = false;
            }
            st.Location =Convert.ToInt32(form["cityId"]);
            st.Email = form["Email"];
           
            st.Link = form["Link"];
            st.RealName = form["RealName"];
            st.HourFrom = form["HourFrom"];
            st.HourTo = form["HourTo"];
            st.Services = form["Services"];
            st.DayOff = form["DayOff"];
            st.Additional = form["Additional"];
            st.PictureMobile = PictureMobile.FileName;
            
            string path = Path.Combine(Server.MapPath("~/Images"),
                                         Path.GetFileName(Picture.FileName));
            Picture.SaveAs(path);
            st.MapPicture = MapPicture.FileName;// form["MapPicture"];
            string path1 = Path.Combine(Server.MapPath("~/Images"),
                                         Path.GetFileName(MapPicture.FileName));
            MapPicture.SaveAs(path1);
            st.Picture = Picture.FileName;//form["Picture"];
            string path2 = Path.Combine(Server.MapPath("~/Images"),
                                         Path.GetFileName(PictureMobile.FileName));
            PictureMobile.SaveAs(path2);
            db.Stores.Add(st);
            db.SaveChanges();
            return RedirectToAction("Stores");
    
        }
        public ActionResult EditStore(int? id)
        {

            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Store store = db.Stores.Find(id);

                Location loc = new Location();
                ViewBag.loc = from l in db.Locations
                                    select l;
                return View(store);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditStore(FormCollection form, HttpPostedFileBase Picture, HttpPostedFileBase MapPicture, HttpPostedFileBase PictureMobile)
        {
            
            int id = Convert.ToInt32(form["Id"]);

            Store st = db.Stores.Find(id);
           
            st.Name = form["Name"];
            st.Street = form["Street"];
            st.Latitude = form["Latitude"];
            st.Longitude = form["Longitude"];
            st.Type = form["Type"];
            st.Latitude = form["Latitude"];
            st.WorkingHours = form["WorkingHours"];
            st.Price = form["Price"];
            
            if (form["Partner"] == "1")
            {
                st.Partner = true;
            }
            else
            {
                st.Partner = false;
            }
            st.Location = Convert.ToInt32(form["cityId"]);
            st.Email = form["Email"];
            
            st.Link = form["Link"];
            st.RealName = form["RealName"];
            st.HourFrom = form["HourFrom"];
            st.HourTo = form["HourTo"];
            st.Services = form["Services"];
            st.DayOff = form["DayOff"];
            st.Additional = form["Additional"];
            if (Picture != null)
            {
                st.Picture = Picture.FileName;//form["Picture"];
                string path = Path.Combine(Server.MapPath("~/Images"),
                                             Path.GetFileName(Picture.FileName));
                Picture.SaveAs(path);
            }
            if (MapPicture != null)
            {
                st.MapPicture = MapPicture.FileName;// form["MapPicture"];
                string path1 = Path.Combine(Server.MapPath("~/Images"),
                                             Path.GetFileName(MapPicture.FileName));
                MapPicture.SaveAs(path1);
            }
            if (PictureMobile != null)
            {
                st.PictureMobile = PictureMobile.FileName;
                string path2 = Path.Combine(Server.MapPath("~/Images"),
                                             Path.GetFileName(PictureMobile.FileName));
                PictureMobile.SaveAs(path2);
            }
            
            
            db.Entry(st).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Stores");

        }
        public ActionResult DeleteStore(int? id)
        {
            if (HttpContext.Request.Cookies["admin"] == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Store st = db.Stores.Find(id);

                return View(st);
            }
        }

        [HttpPost, ActionName("DeleteStore")]
        [ValidateInput(false)]
        public ActionResult DeleteConfirmedStore(FormCollection form)
        {

            int id = Convert.ToInt32(form["id"]);

            Store st = db.Stores.Find(id);

            db.Stores.Remove(st);
            db.SaveChanges();

            return RedirectToAction("Stores");
        }
    }
}