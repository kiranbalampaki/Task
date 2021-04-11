using NBITTask.Models;
using NBITTask.Models.Entities;
using NBITTask.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NBITTask.Controllers
{
    public class ReviewsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Reviews
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Save(ProductReviewVM productReviewVM)
        {
            try
            {
                Review review = new Review();
                review.CustomerReview = productReviewVM.Review.CustomerReview;
                review.ProductId = productReviewVM.Product.Id;

                var product = db.Products.Find(productReviewVM.Product.Id);
                review.Product = product;

                var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();
                review.UserId = user.Id;
                review.User = user;
                review.ReviewDateTime = DateTime.Now;

                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View(productReviewVM);
            }
        }
    }
}