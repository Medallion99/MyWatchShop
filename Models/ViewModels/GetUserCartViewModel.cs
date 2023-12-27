using MyWatchShop.Models.Entity;

namespace MyWatchShop.Models.ViewModels
{
    public class GetUserCartViewModel
    {
        public int Quantity { get; set; }

        public string ShoppingCartId { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
