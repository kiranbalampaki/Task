namespace NBITTask.Models.Entities
{
    public class Rating
    {
        public int Id { get; set; }
        public int ProductRating { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}