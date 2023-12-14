using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.DTOS;
using MyWatchShop.Models.Entity;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        private readonly ShopDbContext _ctx;

        public ProductService(IRepository repository, ShopDbContext ctx)
        {
            this._repository = repository;
            this._ctx = ctx;
        }
        public Task<int> AddProduct(AddProductViewModel model)
        {

            var product = new Product
            {
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                Price = model.Price,
                ImageUrl = model.ImageUrl,
            };

            var addProduct = _repository.Add(product);

            if (addProduct == null)
            {
                throw new NullReferenceException("Product could not be added");
            }

            return addProduct;
        }

        public Task<int> DeleteProduct(string id)
        {

            try
            {
                var productToDelete = _ctx.Products.FirstOrDefault(s => s.Id == id);

               
                if (productToDelete == null)
                {
                    throw new Exception("No suchh product exists");
                }

                var delete = _repository.Delete(productToDelete);

                if (delete == null)
                {
                    throw new Exception("Sorry Something Went Wrong");
                }

                return delete;

            }
            catch (Exception ex)
            {
                throw new Exception ("Sorry, something went wrong");
            }
        }

        public Task<IList<AddProductViewModel>> GetAllProduct(AddProductViewModel model)
        {
            var productsToReturn = _repository.GetAll<Product>();

            
            throw new NotImplementedException();
        }

        public Task<AddProductViewModel> GetProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateProduct(AddProductViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
