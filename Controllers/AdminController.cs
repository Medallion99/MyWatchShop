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
        public IActionResult WelcomePage()
        {
            return View();
        }
        [HttpGet]
        [Authorize]
        public IActionResult Add ()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            var result = await _adminServices.AddProduct(model);
            return RedirectToAction("BestSeller", "Home");
        }

        public IActionResult ManageUser()
        {
            return View();
        }
        public IActionResult ManageRole()
        {
            return View();
        }
        public IActionResult ManageProduct()
        {
            return View();
        }
    }
}
