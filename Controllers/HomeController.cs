using Microsoft.AspNetCore.Mvc;
using MyWatchShop.Models;
using MyWatchShop.Models.ViewModels;
using System.Diagnostics;

namespace MyWatchShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel();
            homeViewModel.BestSeller = new Showcase
            {
                Product = 4,
                ProductList = new List<ProductSummarizedViewModel>()
                {
                    new ProductSummarizedViewModel
                    {
                        ProductName  = "Rolex",
                        ImageUrl = "",
                        OldPrice = 60000,
                        NewPrice = 50000,
                        Stars = 5,
                    },
                    new ProductSummarizedViewModel
                    {
                        ProductName  = "Omega",
                        ImageUrl = "",
                        OldPrice = 32000,
                        NewPrice = 25000,
                        Stars = 5,
                    },
                    new ProductSummarizedViewModel
                    {
                        ProductName  = "Patek Philip",
                        ImageUrl = "",
                        OldPrice = 50000,
                        NewPrice = 40000,
                        Stars = 5,
                    }, 
                    new ProductSummarizedViewModel
                    {
                        ProductName  = "Cartier",
                        ImageUrl = "",
                        OldPrice = 25000,
                        NewPrice = 20000,
                        Stars = 4,
                    }
                }
            };

            return View(homeViewModel);
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
