using NBITTask.Models;
using NBITTask.Models.Entities;
using NBITTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NBITTask.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Products
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Save(int Id = 0)
        {
            Product product = new Product();
            if (Id != 0)
            {
                product = db.Products.Find(Id);
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Save(Product product, HttpPostedFileBase productPhoto)
        {
            try
            {
                db.Entry(product).State = EntityState.Modified;
                if (product.Id != 0)
                {
                    if (productPhoto != null)
                    {
                        product.Image = new byte[productPhoto.ContentLength];
                        productPhoto.InputStream.Read(product.Image, 0, productPhoto.ContentLength);
                    }
                }
                else
                {
                    product.Image = new byte[productPhoto.ContentLength];
                    productPhoto.InputStream.Read(product.Image, 0, productPhoto.ContentLength);
                    db.Products.Add(product);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(product);
            }
        }
        public ActionResult Details(int? Id)
        {
            ProductReviewVM productReviewVM = new ProductReviewVM();
            if (Id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Products.Where(x => x.Id == Id).Include(x => x.Reviews).SingleOrDefault();
            Product product = db.Products.Where(x => x.Id == Id).SingleOrDefault();
            List<Review> reviews = db.Reviews.Where(x => x.ProductId == Id).Include(x => x.User).ToList();
            List<Rating> ratings = db.Rating.Where(x => x.ProductId == Id).ToList();

            if (User.Identity.IsAuthenticated)
            {
                var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
                var userRating = db.Rating.Where(x => x.ProductId == Id && x.UserId == user.Id).SingleOrDefault();
                if (userRating != null)
                {
                    productReviewVM.userRating = userRating.ProductRating;
                }
            }

            if (ratings.Count != 0)
            {
                int totalrating = 0;
                foreach (var item in ratings)
                {
                    totalrating += item.ProductRating;
                }

                var averageRating = totalrating / ratings.Count;
                string quality;
                if (averageRating <= 1.6)
                {
                    quality = "Worst";
                }
                else if (averageRating > 1.6 && averageRating <= 3.2)
                {
                    quality = "Bad";
                }
                else if (averageRating > 3.2)
                {
                    quality = "Good";
                }
                else
                {
                    quality = "Not Yet Rated";
                }

                ViewBag.quality = quality;
                ViewBag.averageRating = averageRating;
            }

            product.Reviews = reviews;
            productReviewVM.Product = product;

            return View(productReviewVM);
        }
    }
}