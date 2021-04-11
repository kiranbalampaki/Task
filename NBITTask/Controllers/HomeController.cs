using NBITTask.Models;
using NBITTask.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NBITTask.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var Products = db.Products.ToList();
            foreach (var item in Products)
            {
                List<Rating> ratings = db.Rating.Where(x => x.ProductId == item.Id).ToList();
                item.Ratings = ratings;
            }

            return View(Products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}