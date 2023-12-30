using Microsoft.AspNetCore.Authorization;
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddItem(string productId, int qty = 1, int redirect = 0)
        {
            var cartCount = await _cartService.AddItem(productId, qty);
            if (redirect == 0)
            {
                return RedirectToAction("BestSeller", "Product");
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
            var cart = await _cartService.GetUserCart();
            return View(cart);
        }

        public async Task<IActionResult> GetTotalItemInCart()
        {
            var cartItemCount = await _cartService.GetCartItemCount("");
            return Ok(cartItemCount);
        }

        public async Task<IActionResult> UpdateQuantity (string productId, int qty)
        {
            var quantity = await _cartService.UpdateCartQty(productId, qty);
            return RedirectToAction("GetUserCart");
        }
    }
}
