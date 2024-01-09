using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.Entity;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _repository;
        private readonly ShopDbContext _ctx;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartService _cartService;

        public OrderService(IRepository repository, ShopDbContext ctx, UserManager<AppUser> userManager, ICartService cartService)
        {
            this._repository = repository;
            this._ctx = ctx;
            this._userManager = userManager;
            this._cartService = cartService;
        }

        

        public async Task<IList<Order>> UserOrders(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var orders = await _ctx.Orders
                            .Include(s => s.OrderStatus)
                            .Include(s => s.OrderDetails)
                            .ThenInclude(s => s.Product)
                            .Where(a => a.AppUserId == userId)
                            .ToListAsync();
            return orders;
        }

        public async Task<bool> PlaceOrder()
        {
            try
            {
                var userId = _cartService.GetUserId();

                var userOrder = await _ctx.Orders.FirstOrDefaultAsync(s => s.Id == userId);

                var orders = await _ctx.OrderDetails.Where(s => s.OrderId == userOrder.Id).ToListAsync();
                //var orders = await _repository.GetAll<OrderDetail>();
                if (orders == null)
                {
                    throw new Exception("No items found");
                }
                var removeOrder = await _repository.RemoveRange<OrderDetail>(orders);
                
                if(removeOrder != null)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
    }
}
