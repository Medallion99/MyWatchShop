using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.Entity;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Services.Implementation
{
    public class OrderService
    {
        private readonly IRepository _repository;
        private readonly ICartService _cartService;
        private readonly ShopDbContext _ctx;

        public OrderService(IRepository repository, ICartService cartService, ShopDbContext ctx)
        {
            this._repository = repository;
            this._cartService = cartService;
            this._ctx = ctx;
        }

        public async Task CheckOut()
        {
            using var transaction = _ctx.Database.BeginTransaction();
            try
            {
                //Moving data from cartdetail to order and order detail, then we will remove cart detail
                var userId = _cartService.GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not found");

                var cart = await _cartService.GetCart(userId);
                if (cart == null)
                    throw new Exception("No Cart Item Found");

                var cartDetails = _ctx.CartDetails.Where(s => s.ShoppingCartId == cart.Id).ToList();

                if(cartDetails.Count == 0)
                {
                    throw new Exception("Cart is Empty");
                }

                var order = new Order
                {
                    AppUserId = userId,

                };
        }
    }
}
