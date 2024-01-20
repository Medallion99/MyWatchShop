using Microsoft.AspNetCore.Mvc;
using MyWatchShop.Data.Repository.Interface;
using MyWatchShop.Models.Entity;
using MyWatchShop.Services.Implementation;
using MyWatchShop.Services.Interfaces;

namespace MyWatchShop.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IRepository _repository;

        public OrderController(IOrderService orderService, IRepository repository)
        {
            this._orderService = orderService;
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> UserOrders(string userId)
        {
            var orders = await _orderService.UserOrders(userId);
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> PlaceOrder(string userId)
        {
            //var result = _orderService.PlaceOrder();

            //if(result != null)
            //{
            //    return RedirectToAction("BestSeller", "Home");
            //}
            //return View("UserOrders");

            //var userId = _cartService.GetUserId();

            //var userOrder = await _ctx.Orders.FirstOrDefaultAsync(s => s.Id == userId);

            //var orders = await _ctx.OrderDetails.Where(s => s.OrderId == userOrder.Id).ToListAsync();
            //var orders = await _repository.GetAll<OrderDetail>();
            //if (orders == null)
            //{
            //    throw new Exception("No items found");
            //}
            //var removeOrder = await _repository.RemoveRange<OrderDetail>(orders);

            //if (removeOrder != null)
            //{
            //    return View("UserOrders");
            //}
            //return View("UserOrders");
            var producttoreturn = await _orderService.PlaceOrder();

            if(producttoreturn != null)
            {
                return View("UserOrders");
            }
            return RedirectToAction("BestSeller", "Home");
        }
    }
}
