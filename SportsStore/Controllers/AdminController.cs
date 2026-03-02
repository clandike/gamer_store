using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using GamerStore.Data.Repository;
using GamerStore.Models;
using GamerStore.Models.ViewModels;

namespace GamerStore.Controllers
{
    [Authorize]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IStoreRepository storeRepository;
        private readonly IOrderRepository orderRepository;

        public AdminController(IStoreRepository storeRepository, IOrderRepository orderRepository)
        {
            this.storeRepository = storeRepository;
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("Orders")]
        public ViewResult Orders() => this.View(this.orderRepository.Orders);

        [HttpGet]
        [Route("Products")]
        public ViewResult Products() => this.View(this.storeRepository.Products);

        [HttpPost]
        [Route("MarkShipped")]
        public IActionResult MarkShipped(int orderId)
        {
            if (this.ModelState.IsValid)
            {
                Order? order = this.orderRepository.Orders.FirstOrDefault(o => o.OrderId == orderId);

                if (order != null)
                {
                    order.Shipped = true;
                    this.orderRepository.SaveOrder(order);
                }
            }

            return this.RedirectToAction("Orders");
        }

        [HttpGet]
        [Route("Details/{productId:int}")]
        public ViewResult Details(int productId)
        {
            Product product = new Product();

            if (this.ModelState.IsValid)
            {
                product = this.storeRepository.Products.FirstOrDefault(p => p.Id == productId) !;
            }

            return this.View(product);
        }

        [HttpPost]
        [Route("Reset")]
        public IActionResult Reset(int orderId)
        {
            Order? order = this.orderRepository.Orders
                .FirstOrDefault(o => o.OrderId == orderId);

            if (order != null && this.ModelState.IsValid)
            {
                order.Shipped = false;
                this.orderRepository.SaveOrder(order);
            }

            return this.RedirectToAction("Orders");
        }

        [HttpGet]
        [Route("Products/Edit/{productId:long}")]
        public ViewResult Edit(int productId)
        {
            Product? product = null;

            if (this.ModelState.IsValid)
            {
                product = this.storeRepository.Products.FirstOrDefault(p => p.Id == productId) !;
            }

            return this.View(product);
        }

        [HttpPost]
        [Route("Products/Edit/{productId:long}")]
        public IActionResult Edit(Product product)
        {
                this.storeRepository.SaveProduct(product);
                return RedirectToAction("Products");
        }

        [HttpGet("Products/Create")]
        public ViewResult Create()
        {
            return this.View(new Product());
        }

        [HttpPost]
        [Route("Products/Create")]
        public IActionResult Create(Product product)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(product);
            }

            this.storeRepository.SaveProduct(product);
            return this.RedirectToAction("Products");
        }

        [HttpGet]
        [Route("Products/Delete/{productId:long}")]
        public IActionResult Delete(int productId)
        {
            Product model = new Product();
            if (this.ModelState.IsValid)
            {
                model = this.storeRepository.Products.FirstOrDefault(p => p.Id == productId) !;
            }

            return this.View(model);
        }

        [HttpPost]
        [Route("Products/Delete/{productId:long}")]
        public IActionResult DeleteProduct(int productId)
        {
            if (this.ModelState.IsValid)
            {
                var product = this.storeRepository.Products.FirstOrDefault(p => p.Id == productId);
                this.storeRepository.DeleteProduct(product!);
            }

            return this.RedirectToAction("Products");
        }
    }
}
