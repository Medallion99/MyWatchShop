using MyWatchShop.Models.DTOS;
using MyWatchShop.Models.Entity;

namespace MyWatchShop.Services.Interfaces
{
    public interface IProductService
    {
        public Task<int> AddProduct(AddProductViewModel model);
        public Task<int> UpdateProduct(AddProductViewModel model);
        public Task<IList<Product>> GetAllProduct(AddProductViewModel model);
        public Task<Product> GetProduct(string id);
        public Task<int> DeleteProduct(string id);
    }
}
