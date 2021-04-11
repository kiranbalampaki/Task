﻿using NBITTask.Models;
using NBITTask.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NBITTask.Controllers
{
    public class RatingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Ratings
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddRating(int productRating, int productId)
        {
            Rating rating = new Rating();
            rating.ProductRating = productRating;

            var product = db.Products.Find(productId);
            var user = db.Users.Where(m => m.Email == User.Identity.Name).FirstOrDefault();

            rating.Product = product;
            rating.ProductId = product.Id;
            rating.User = user;
            rating.UserId = user.Id;

            db.Rating.Add(rating);
            db.SaveChanges();
            return RedirectToAction("Details", "Product", productId);
            //return Json().ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}