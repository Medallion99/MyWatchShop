using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Models.Entity;
using MyWatchShop.Services.Interfaces;
using AutoMapper;

namespace MyWatchShop.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository _repository;
        private readonly ShopDbContext _ctx;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public ProductService(IRepository repository, ShopDbContext ctx, IMapper mapper, IUploadService uploadService)
        {
            _repository = repository;
            _ctx = ctx;
            _mapper = mapper;
            _uploadService = uploadService;
        }
        public async Task<int> AddProduct(AddProductViewModel model)
        {
            //var product = _mapper.Map<Product>(model);
            var photoUpload = await _uploadService.ImageUpload(model.photoFile, "");
            var product = new Product
            {
                ProductName = model.ProductName,
                ProductDescription = model.ProductDescription,
                OldPrice = model.OldPrice,
                NewPrice = model.NewPrice,
                ImageUrl = photoUpload["Url"],
                Stars = model.Stars,
            };

            var addProduct = await _repository.Add<Product>(product);

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

        public async Task<IList<Product>> GetAllProduct()
        {
            var productsToReturn = await _repository.GetAll<Product>();
            
            return productsToReturn;
        }

        public Task<Product> GetProduct(string id)
        {
            var getBook = _repository.GetById<Product>(id);
            return getBook;

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
