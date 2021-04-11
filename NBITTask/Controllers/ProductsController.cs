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
            Product product = db.Products.Where(x => x.Id == Id).Include(x => x.Reviews).SingleOrDefault();
            productReviewVM.Product = product;
            //productReviewVM.Review = product.Re;
            //if (Id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            ////Product product = db.Products.Find(Id);
            ////Product product = db.Products.Where(x => x.Id == Id).Include(x => x.Reviews).SingleOrDefault();
            //if (product == null)
            //{
            //    return HttpNotFound();
            //}

            return View(productReviewVM);
        }
    }
}