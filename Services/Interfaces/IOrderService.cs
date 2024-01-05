namespace MyWatchShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> CheckOut();
    }
}
