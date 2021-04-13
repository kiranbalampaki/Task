using NBITTask.Models;
using NBITTask.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NBITTask.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Reports
        public ActionResult Index()
        {
            var Products = db.Products.ToList();
            foreach (var item in Products)
            {
                List<Rating> ratings = db.Rating.Where(x => x.ProductId == item.Id).ToList();
                List<Review> reviews = db.Reviews.Where(x => x.ProductId == item.Id).ToList();
                item.Ratings = ratings;
                item.Reviews = reviews;
            }
            return View(Products);
        }
    }
}