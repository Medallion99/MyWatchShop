using MyWatchShop.Models.Entity;
using MyWatchShop.Models.ViewModels;

namespace MyWatchShop.Services.Interfaces
{
    public interface ICartService
    {
        Task<int> AddItem(string productId, int qty);
        Task<GetUserCartViewModel> GetUserCart();
        Task<int> RemoveItem(string productId);
        Task<int> GetCartItemCount(string userId);
        Task<ShoppingCart> GetCart(string userId);
        string GetUserId();
    }
}