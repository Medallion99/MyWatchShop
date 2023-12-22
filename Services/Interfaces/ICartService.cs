using MyWatchShop.Models.Entity;
using MyWatchShop.Models.ViewModels;

namespace MyWatchShop.Services.Interfaces
{
    public interface ICartService
    {
        public Task<int> AddToCart(Product model);
        public Task<IList<Product>> GetAll();
        public Task<int> DeleteProduct(string id);
    }
}
