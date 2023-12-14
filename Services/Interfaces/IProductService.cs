using MyWatchShop.Models.DTOS;

namespace MyWatchShop.Services.Interfaces
{
    public interface IProductService
    {
        public Task<int> AddProduct(AddProductViewModel model);
        public Task<int> UpdateProduct(AddProductViewModel model);
        public Task<IList<AddProductViewModel>> GetAllProduct(AddProductViewModel model);
        public Task<AddProductViewModel> GetProduct(string id);
        public Task<int> DeleteProduct(string id);
    }
}
