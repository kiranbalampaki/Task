using System;
using System.ComponentModel.DataAnnotations;

namespace NBITTask.Models.Entities
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string CustomerReview { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime ReviewDateTime { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}