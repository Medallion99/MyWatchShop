using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;

        public CartController(ICartService cartService, IOrderService orderService)
        {
            this._cartService = cartService;
            this._orderService = orderService;
        }
        //[HttpGet]
        [Authorize]
        public async Task<IActionResult> AddItem(string productId ="", int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartService.AddItem(productId, qty);
            if (redirect == 0)
            {
                return RedirectToAction("BestSeller", "Home");
            }
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> RemoveItem (string productId)
        {
            var cartCount = await _cartService.RemoveItem(productId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            try
            {
                var cart = await _cartService.GetUserCart();
                return View(cart);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IActionResult> GetTotalItemInCart(string userId)
        {
            var cartItemCount = await _cartService.GetCartItemCount(userId);
            return Ok(cartItemCount);
        }
        public async Task<IActionResult> CheckOut()
        {
            bool checkOut = await _orderService.CheckOut();
            if (!checkOut)
            {
                throw new Exception("Internal Error Occured");
            }
            return RedirectToAction("BestSeller", "Home");
        }

        public async Task<IActionResult> UpdateQuantity (string productId, int qty)
        {
            var quantity = await _cartService.UpdateCartQty(productId, qty);
            return RedirectToAction("GetUserCart");
        }
    }
}
