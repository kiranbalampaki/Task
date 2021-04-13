using NBITTask.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBITTask.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public List<Review> reviews { get; set; }
        public int averageRating { get; set; }
        public string quality { get; set; }
    }
}