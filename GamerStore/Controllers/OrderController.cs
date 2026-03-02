using Microsoft.AspNetCore.Mvc;
using GamerStore.Data.Repository;
using GamerStore.Models;

namespace GamerStore.Controllers
{
    [Route("Order")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;

        private readonly Cart cart;

        public OrderController(IOrderRepository orderRepository, Cart cart)
        {
            this.orderRepository = orderRepository;
            this.cart = cart;
        }

        [HttpGet]
        [Route("Checkout")]
        public ViewResult Checkout() => this.View(new Order());

        [HttpPost]
        [Route("Checkout")]
        public IActionResult Checkout(Order order)
        {
            if (!this.cart.Lines.Any())
            {
                this.ModelState.AddModelError(key: string.Empty, errorMessage: "Sorry, your cart is empty!");
            }

            if (this.ModelState.IsValid)
            {
                ArgumentNullException.ThrowIfNull(order);
                order.Lines = this.cart.Lines.ToArray();
                this.orderRepository.SaveOrder(order);
                this.cart.Clear();
                return this.View("Completed", order.OrderId);
            }

            return this.View();
        }
    }
}
