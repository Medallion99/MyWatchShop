namespace MyWatchShop.Models.Entity
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public IList<Review> Reviews { get; set; } = new List<Review>();
        public IList<Rating> Ratings { get; set; } = new List<Rating>();

    }
}
