namespace Task.Entity.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string CustomerReview { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
