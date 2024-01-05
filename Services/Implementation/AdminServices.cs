using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Models.Entity;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Services.Implementation
{
    public class AdminServices : IAdminServices
    {
        private readonly ShopDbContext _ctx;
        private readonly IRepository _repository;

        public AdminServices(ShopDbContext ctx, IRepository repository)
        {
            this._ctx = ctx;
            this._repository = repository;
        }
        public async Task<int> AddProduct(AddProductViewModel model)
        {
            try
            {
                var product = new Product()
                {
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    OldPrice = model.OldPrice,
                    NewPrice = model.NewPrice,
                    ImageUrl = model.ImageUrl,
                };
                var result = await _repository.Add<Product>(product);
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }   

        public Task<int> DeleteProduct(string id)
        {
            var result = _ctx.Products.Where(s => s.Id == id).FirstOrDefault();

            if (result == null)
            {
                throw new Exception("ID not found");
            }
            var resultToReturn = _repository.Delete(result);
            return resultToReturn;
        }

        public async Task<IList<Product>> GetAllProduct()
        {
            var productsToReturn = await _repository.GetAll<Product>();

            return productsToReturn;
        }

        public Task<Product> GetProduct(string id)
        {
            var result = _ctx.Products.Where(_ => _.Id == id).FirstOrDefault();

            if (result == null)
            {
                throw new Exception("Invalid Id");
            }
            return _repository.GetById<Product>(id);
        }

        public Task<int> UpdateProduct(AddProductViewModel model)
        {
            var product = new Product()
            {
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                OldPrice = model.OldPrice,
                NewPrice = model.NewPrice,
                ImageUrl = model.ImageUrl,
            };

            var result = _repository.Update(model);
            return result;
        }
    }
}
