using GamerStore.Models;
using GamerStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerStore.Controllers
{
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly IOrderService service;

        private readonly Cart cart;

        public OrderController(IOrderService service, Cart cart)
        {
            this.service = service;
            this.cart = cart;
        }

        [HttpGet]
        [Route("Checkout")]
        public ViewResult Checkout() => this.View(new Order());

        [HttpPost]
        [Route("Checkout")]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (!this.cart.Lines.Any())
            {
                this.ModelState.AddModelError(key: string.Empty, errorMessage: "Sorry, your cart is empty!");
            }
            else
            {
                await this.service.SaveOrderAsync(order, this.cart.Lines);
                this.cart.Clear();
                return this.View("Completed", order.OrderId);
            }

            return this.View();
        }

        [HttpPost]
        [Route("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(int orderId, OrderStatus newStatus, string returnUrl)
        {
            try
            {
                await this.service.ChangeStatusAsync(orderId, newStatus);
                return this.Redirect(returnUrl);
            }
            catch (InvalidOperationException ex)
            {
                return this.BadRequest(ex.Message);
            }
        }
    }
}
