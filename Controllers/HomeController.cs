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
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;

        public HomeController(ILogger<HomeController> logger, IProductService productService)
        {
            _logger = logger;
            this._productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var viewToReturn = await _productService.GetAllProduct();
            var homeView = new HomeViewModel();


            homeView.BestSeller = new Showcase
            {
                Product = 4,
                ProductList = new List<ProductSummarizedViewModel>()
            };

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
