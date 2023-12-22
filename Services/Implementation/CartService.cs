using Microsoft.AspNetCore.OutputCaching;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.Entity;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IRepository _repository;
        private readonly IProductService _productService;

        public CartService(IRepository repository, IProductService productService)
        {
            this._repository = repository;
            this._productService = productService;
        }
        public async Task<int> AddToCart(Product model)
        {
            var product = await _productService.GetProduct(model.Id);

            var view = new CartViewModel();
            view.Carts.Add(new Cart
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                OldPrice = product.OldPrice,
                NewPrice = product.NewPrice,
                ImageUrl = product.ImageUrl,
            });

            var result = await _repository.Add(view);
            if (result == null)
            {
                throw new NullReferenceException("Product could not be added");
            }

            return result;


            //var addToCart = await _repository.Add<Cart>(product);
        }

        public Task<int> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Product>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
