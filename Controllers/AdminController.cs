using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.Entity;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Controllers
{
    [Authorize]

    public class AdminController : Controller
    {
        private readonly IAdminServices _adminServices;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository _repository;
        private readonly ShopDbContext _ctx;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(IAdminServices adminServices, UserManager<AppUser> userManager, IRepository repository, ShopDbContext ctx, RoleManager<IdentityRole> roleManager)
        {
            this._adminServices = adminServices;
            this._userManager = userManager;
            this._repository = repository;
            this._ctx = ctx;
            this._roleManager = roleManager;
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
        [AcceptVerbs("Post", "Get")]
        public async Task<IActionResult> ManageUser(ManageUserViewModel model, string userId)
        {
            var user = await _adminServices.GetAllUsers(model, userId);

            return View(user);
        }
        [AcceptVerbs("Post", "Get")]
        public async Task<IActionResult> ManageRole(ManageRoleViewModel model)
        {
            if (!string.IsNullOrEmpty(model.RoleName))
            {
                var allRole = _roleManager.Roles;
                if (!allRole.Any(x => x.Name.ToLower() == model.RoleName.ToLower()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(model.RoleName));
                }
            }

            var role = _roleManager.Roles;
            var manageRoleViewModel = new ManageRoleViewModel();
            if(role != null && role.Any())
            {
                manageRoleViewModel.RolesToReturn = role.Select(s => new RolesToReturnViewModel
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList();
            }
            return View(manageRoleViewModel);
        }
        public IActionResult ManageProduct()
        {
            return View();
        }

        public async Task<IActionResult> DeleteUser(string userId)
        {
            //var userToDelete = _adminServices.DeleteUser(userId);
            //if (userToDelete != null)
            //{
            //    return RedirectToAction("ManageUser");
            //}
            //return View();
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var cartToDelete = await _ctx.ShoppingCarts.FirstOrDefaultAsync(s => s.AppUserId == userId);
                var deleteCart = await _repository.Delete<ShoppingCart>(cartToDelete);

                var deleteUser = await _userManager.DeleteAsync(user);
                if (deleteUser.Succeeded)
                {
                    return RedirectToAction("ManageUser");

                }

            }
            return View();

        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var roles = await _roleManager.FindByIdAsync(roleId);
            if (roles != null)
            {
                //var cartToDelete = await _ctx.ShoppingCarts.FirstOrDefaultAsync(s => s.AppUserId == userId);
                //var deleteCart = await _repository.Delete<ShoppingCart>(cartToDelete);

                var deleteRole = await _roleManager.DeleteAsync(roles);
                if (deleteRole.Succeeded)
                {
                    return RedirectToAction("ManagerRole");
                }

            }
            return View();

        }
    }
}
