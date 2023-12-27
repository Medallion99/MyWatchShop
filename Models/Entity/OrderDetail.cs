namespace MyWatchShop.Models.Entity
{
    public class OrderDetail : BaseEntity
    {
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
