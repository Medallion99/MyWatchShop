using Microsoft.AspNetCore.Mvc;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            this._cartService = cartService;
        }
        public async Task<IActionResult> AddItem(string productId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartService.AddItem(productId, qty);
            if (redirect == 0)
            {
                return Ok(cartCount);
            }
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> RemoveItem (string productId)
        {
            var cartCount = _cartService.RemoveItem(productId);
            return RedirectToAction("GetUserCart");
        }

        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartService.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            var cartItemCount = await _cartService.GetCartItemCount("");
            return Ok(cartItemCount);
        }
    }
}
