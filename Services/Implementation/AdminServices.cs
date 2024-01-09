using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Models.Entity;
using MyWatchShop.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MyWatchShop.Services.Implementation
{
    [Authorize(Roles = "admin")]

    public class AdminServices : IAdminServices
    {
        private readonly ShopDbContext _ctx;
        private readonly IRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        //private readonly RoleManager<AppUser> _roleManager;

        public AdminServices(ShopDbContext ctx, IRepository repository, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._ctx = ctx;
            this._repository = repository;
            this._userManager = userManager;
            this._roleManager = roleManager;
            //this._roleManager = roleManager;
        }
        public async Task<int> AddProduct(AddProductViewModel model)
        {
            try
            {
                var product = new Product()
                {
                    ProductName = model.ProductName,
                    ProductDescription = model.ProductDescription,
                    OldPrice = model.OldPrice,
                    NewPrice = model.NewPrice,
                    ImageUrl = model.ImageUrl,
                };
                var result = await _repository.Add<Product>(product);
                return result;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }   

        public Task<int> DeleteProduct(string id)
        {
            var result = _ctx.Products.Where(s => s.Id == id).FirstOrDefault();

            if (result == null)
            {
                throw new Exception("ID not found");
            }
            var resultToReturn = _repository.Delete(result);
            return resultToReturn;
        }

        public async Task<IList<Product>> GetAllProduct()
        {
            var productsToReturn = await _repository.GetAll<Product>();

            return productsToReturn;
        }

        public Task<Product> GetProduct(string id)
        {
            var result = _ctx.Products.Where(_ => _.Id == id).FirstOrDefault();

            if (result == null)
            {
                throw new Exception("Invalid Id");
            }
            return _repository.GetById<Product>(id);
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

        public async Task<ManageUserViewModel> GetAllUsers(ManageUserViewModel model, string userId)
        {

            if (!string.IsNullOrEmpty(model.RoleName))
            {
                var selectedUser = await _userManager.FindByIdAsync(userId);
                var userRoles = await _userManager.GetRolesAsync(selectedUser);
                if (!userRoles.Any(x => x.ToLower() == model.RoleName.ToLower()))
                {
                    await _userManager.AddToRoleAsync(selectedUser, model.RoleName);
                }
            }

            var users = _userManager.Users;
            var manageUserViewModel = new ManageUserViewModel();
            if(users != null && users.Any())
            {
                manageUserViewModel.TableData = users.Select(x => new UserToReturnViewModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Email = x.Email,
                }).ToList();

                if(!string.IsNullOrEmpty(userId))
                {
                    var user = manageUserViewModel.TableData.FirstOrDefault(s => s.Id == userId);
                    if (user != null)
                    {
                        var userRole = await _userManager.GetRolesAsync(users.FirstOrDefault(x => x.Id == userId));
                        user.Roles = userRole.ToList();
                        manageUserViewModel.UserDetails = user;
                    }
                }
            }
            return manageUserViewModel;
        }

        public async Task<bool> DeleteUser(string userId)
        {
            //var cart = _repository.GetAll<ShoppingCart>();

            var user = await _userManager.FindByIdAsync(userId);
            if(user != null)
            {
                var cartToDelete = await _ctx.ShoppingCarts.FirstOrDefaultAsync(s => s.AppUserId == userId);
                var deleteCart = await _repository.Delete<ShoppingCart>(cartToDelete);

                var deleteUser = await _userManager.DeleteAsync(user);
                if(deleteUser.Succeeded)
                {
                    return true;
                }

            }
            return false;
        }
    }
}
