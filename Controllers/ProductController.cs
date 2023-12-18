﻿using Microsoft.AspNetCore.Mvc;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }
        public async Task<IActionResult> BestSeller()
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
    }
}
