namespace MyWatchShop.Models.Entity
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public int Stars { get; set; }

        //Navigation Properties
        public ShoppingCart ShoppingCart { get; set; } = new ShoppingCart();
        public IList<Review> Reviews { get; set; } = new List<Review>();
        public IList<Rating> Ratings { get; set; } = new List<Rating>();
        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail> ();
        public List<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    }
}
