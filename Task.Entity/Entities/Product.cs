using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task.Entity.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
