namespace MyWatchShop.Models.Entity
{
    public class CartDetail : BaseEntity
    {
        public int Quantity { get; set; }

        public string ShoppingCartId { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
