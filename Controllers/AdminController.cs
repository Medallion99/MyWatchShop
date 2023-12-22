using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminServices _adminServices;

        public AdminController(IAdminServices adminServices)
        {
            this._adminServices = adminServices;
        }
        //public IActionResult AddProduct(AddProductViewModel addProductViewModel)
        //{
        //    var result = _adminServices.AddProduct(addProductViewModel);
        //    return View(result);
        //}
        [HttpGet]
        [Authorize]
        public IActionResult Add ()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(AddProductViewModel model)
        {
            var result = _adminServices.AddProduct(model);
            return RedirectToAction("BestSeller", "Product");
        }
    }
}
