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
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Reports
        public ActionResult Index(ReportVM VM)
        {
            ReportVM reportVM = new ReportVM();
            List<ProductVM> productVMs = new List<ProductVM>();

            var Products = db.Products.ToList();
            foreach (var item in Products)
            {
                ProductVM report = new ProductVM();
                List<Rating> ratings = db.Rating.Where(x => x.ProductId == item.Id).ToList();
                var averageRating = 0;
                string quality = "Not Yet Rated";

                #region Calculate Average Rating & Determine Quality
                var totalrating = 0;
                foreach (var eachRating in ratings)
                {
                    totalrating += eachRating.ProductRating;
                }
                if (ratings.Count != 0)
                {
                    averageRating = totalrating / ratings.Count();

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
                }
                #endregion

                List<Review> reviews = db.Reviews.Where(x => x.ProductId == item.Id).ToList();

                report.Product = item;
                report.averageRating = averageRating;
                report.quality = quality;
                report.reviews = reviews;

                if (VM.quality != null)
                {
                    if (VM.quality == quality)
                    {
                        productVMs.Add(report);
                    }
                }
                else
                {
                    productVMs.Add(report);
                }
            }

            reportVM.products = productVMs;
            reportVM.quality = VM.quality;

            ViewData["qualities"] = new SelectList(new List<SelectListItem>
            {
                new SelectListItem { Text="Good", Value="Good" },
                new SelectListItem { Text="Bad", Value="Bad" },
                new SelectListItem { Text="Worst", Value="Worst" },
            }, "Value", "Text");

            return View(reportVM);
        }
    }
}