using Microsoft.AspNetCore.Mvc;
using MyWatchShop.Models;
using MyWatchShop.Models.Entity;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Services.Interfaces;
using System.Diagnostics;

namespace MyWatchShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(IProductService productService, ICartService cartService)
        {
            this._productService = productService;
            this._cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var viewToReturn = await _productService.GetAllProduct();
            var homeView = new HomeViewModel();

            //homeView.BestSeller = new Showcase
            //{
            //    Product = 4,
            //    ProductList = new List<ProductSummarizedViewModel>()
            //};

            foreach (var product in viewToReturn)
            {
                var productViewModel = new ProductSummarizedViewModel
                {
                    ProductName = product.ProductName,
                    Id = product.Id,
                    OldPrice = product.OldPrice,
                    NewPrice = product.NewPrice,
                    ImageUrl = product.ImageUrl,
                    Stars = product.Stars,
                };
                //var productToRetrieve = viewToReturn.Where(x => x.DateAdded > DateTime.Now.AddDays(-7));

                homeView.BestSeller.ProductList.Add(productViewModel);
            }

            return View(homeView);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
