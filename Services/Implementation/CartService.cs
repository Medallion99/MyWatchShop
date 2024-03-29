﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWatchShop.Data;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.Entity;
using MyWatchShop.Models.ViewModels;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ShopDbContext _ctx;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IRepository _repository;

        public CartService(ShopDbContext ctx, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IRepository repository)
        {
            this._ctx = ctx;
            this._userManager = userManager;
            _contextAccessor = contextAccessor;
            this._repository = repository;
        }


        public async Task<ShoppingCart> GetCart(string userId)
        {
            var cart = await _ctx.ShoppingCarts.FirstOrDefaultAsync(s => s.AppUserId == userId);
            return cart;
        }

        public string GetUserId()
        {
            var principalUser = _contextAccessor.HttpContext.User;
            var userId = _userManager.GetUserId(principalUser);
            return userId;
        }

        public async Task<int> AddItem(string productId, int qty = 0)
        {
            string userId = GetUserId();

            using var transaction = _ctx.Database.BeginTransaction();
            try
            {
                var pId = productId;

                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User id not found");

                var cart = await GetCart(userId);
                if (cart == null)
                {
                    cart = new ShoppingCart
                    {
                        AppUserId = userId,
                    };
                    await _repository.Add<ShoppingCart>(cart);
                    //_ctx.SaveChanges();
                }

                var cartItem = await _ctx.CartDetails.FirstOrDefaultAsync(a => a.ShoppingCartId == cart.Id && a.ProductId == productId);
                if (cartItem != null)
                {
                    cartItem.Quantity += qty;
                }
                else
                {
                    var product = await _repository.GetById<Product>(pId); //Causing me a bug, Will fix later

                    cartItem = new CartDetail
                    {
                        ProductId = productId,
                        Quantity = qty,
                        ShoppingCartId = cart.Id,
                        UnitPrice = product.NewPrice
                    };
                    await _repository.Add<CartDetail>(cartItem);
                }

                transaction.Commit();

            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<int> RemoveItem(string productId)
        {
            string userId = GetUserId();
            try
            {
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User not found");

                var cart = await GetCart(userId);
                if (cart == null)
                {
                    throw new Exception("No corresponding user found");
                }

                var cartItem = _ctx.CartDetails.FirstOrDefault(a => a.ShoppingCartId == cart.Id && a.ProductId == productId);
                if (cartItem == null)
                {
                    throw new Exception("No items in cart");
                }
                else if (cartItem.Quantity == 1)
                {
                    await _repository.Delete<CartDetail>(cartItem);
                }
                else
                {
                    cartItem.Quantity = cartItem.Quantity - 1;
                }
                _ctx.SaveChanges();

            }
            catch (Exception ex)
            {
            }
            var cartItemCount = await GetCartItemCount(userId);
            return cartItemCount;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            //var viewModel = new GetUserCartViewModel();
            var userId = GetUserId();

            if (userId == null)
            {
                throw new Exception("Invalid user id");
            }
            var shoppingCart = await _ctx.ShoppingCarts
                                .Include(a => a.CartDetails)
                                .ThenInclude(a => a.Product)
                                .Where(a => a.AppUserId == userId).FirstOrDefaultAsync();

         
            return shoppingCart;
        }

        public async Task<int> GetCartItemCount(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                userId = GetUserId();
            }
            //var cart = await GetCart(userId);

            //var result = await _ctx.ShoppingCarts.Join(_ctx.CartDetails, cart => cart.Id, cartDetail => cartDetail.ShoppingCartId, (cart, cartDetail) => new
            //{
            //    cart.Id
            //}).ToListAsync();

            //return result.Count;

            var getUserCount = await GetUserCart();
            if(getUserCount == null)
            {
                return 0;
            }

            return getUserCount.CartDetails.Count;
        }

        public async Task<int> UpdateCartQty(string productId, int qty)
        {
            string userId = GetUserId();

            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("User not found");
            }

            var cart = await GetCart(userId);

            if (cart == null)
            {
                throw new Exception("Cart is not found");
            }

            var cartDetail = _ctx.CartDetails.FirstOrDefault(s => s.ShoppingCartId == cart.Id && s.ProductId == productId);

            if (qty >= cartDetail.Quantity)
            {
                cartDetail.Quantity += 1;
            }
            else if(qty > 0 && qty <= cartDetail.Quantity)
            {
                cartDetail.Quantity -= 1;
            }
            else
            {
                throw new Exception("Value cannot be less than 0");
            }
            _ctx.SaveChanges();
            return cartDetail.Quantity;
        }

        public async Task<bool> CheckOut()
        {
            using var transaction = _ctx.Database.BeginTransaction();
            try
            {
                //Moving data from cartdetail to order and order detail, then we will remove cart detail
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not found");

                var cart = await GetCart(userId);
                if (cart == null)
                    throw new Exception("No Cart Item Found");

                var cartDetails = _ctx.CartDetails.Where(s => s.ShoppingCartId == cart.Id).ToList();

                if (cartDetails.Count == 0)
                {
                    throw new Exception("Cart is Empty");
                }

                var order = new Order
                {
                    AppUserId = userId,
                    OrderStatusId = "1" //Pending
                };
                await _repository.Add<Order>(order);

                foreach (var item in cartDetails)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        OrderId = order.Id,
                        UnitPrice = item.UnitPrice,
                    };
                    await _repository.Add<OrderDetail>(orderDetail);
                }
                await _repository.RemoveRange<CartDetail>(cartDetails);
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
