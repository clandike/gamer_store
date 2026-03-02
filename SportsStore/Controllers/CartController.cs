using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using GamerStore.Data.Repository;
using GamerStore.Models;
using GamerStore.Models.ViewModels;

namespace GamerStore.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
        private readonly IStoreRepository repository;

        public CartController(IStoreRepository repository, Cart cart)
        {
            this.repository = repository;
            this.Cart = cart;
        }

        [BindNever]
        public Cart Cart { get; set; }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index(Uri returnUrl)
        {
            CartViewModel viewModel = new CartViewModel();

            if (this.ModelState.IsValid)
            {
                viewModel = new CartViewModel
                {
                    ReturnUrl = returnUrl,
                    Cart = this.Cart,
                };
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("Index")]
        public IActionResult Index(int id, Uri returnUrl)
        {
            Product? product = this.repository.Products.FirstOrDefault(p => p.Id == id);

            if (product != null && this.ModelState.IsValid)
            {
                this.Cart.AddItem(product, 1);

                return this.View(new CartViewModel
                {
                    Cart = this.Cart,
                    ReturnUrl = returnUrl,
                });
            }

            return this.RedirectToPage(returnUrl.AbsolutePath);
        }

        [HttpPost]
        [Route("Remove")]
        public IActionResult Remove(int id, Uri returnUrl)
        {
            CartViewModel model = new CartViewModel();

            if (this.ModelState.IsValid)
            {
                this.Cart.RemoveLine(this.Cart.Lines.First(cl => cl.Product.Id == id).Product);
                model = new CartViewModel
                {
                    Cart = this.Cart,
                    ReturnUrl = returnUrl,
                };
            }

            return this.View("Index", model);
        }
    }
}
