using MyWatchShop.Models.ViewModels;
using MyWatchShop.Models.Entity;

namespace MyWatchShop.Services.Interfaces
{
    public interface IAdminServices
    {
        public Task<int> AddProduct(AddProductViewModel model);
        public Task<int> UpdateProduct(AddProductViewModel model);
        public Task<IList<Product>> GetAllProduct();
        public Task<Product> GetProduct(string id);
        public Task<int> DeleteProduct(string id);
    }
}
