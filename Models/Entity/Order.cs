namespace MyWatchShop.Models.Entity
{
    public class Order : BaseEntity
    {
        public string AppUserId { get; set; }
        public string OrderStatusId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }

    }
}
