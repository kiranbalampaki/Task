using NBITTask.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBITTask.ViewModels
{
    public class ProductReviewVM
    {
        public Product Product { get; set; }
        public Review Review { get; set; }
    }
}