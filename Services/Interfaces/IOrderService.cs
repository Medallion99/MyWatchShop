using MyWatchShop.Models.Entity;

namespace MyWatchShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IList<Order>> UserOrders(string userId);
        Task<bool> PlaceOrder();
    }
}
