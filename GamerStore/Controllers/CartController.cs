using GamerStore.Data.Repository;
using GamerStore.Models;
using GamerStore.Models.ViewModels;
using GamerStore.Services;
using GamerStore.Services.Addiotional;
using GamerStore.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GamerStore.Controllers
{
    [Route("Cart")]
    public class CartController : Controller
    {
    private readonly IProductService productService;
    private readonly IPriceService priceService;

        public CartController(IStoreRepository repository, Cart cart, IProductService productService, IPriceService priceService)
        {
            this.productService = productService;
            this.priceService = priceService;
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
                var customer = new CustomerDTO();

                viewModel = new CartViewModel
                {
                    ReturnUrl = returnUrl,
                    Cart = this.Cart,
                    Pricing = this.priceService.Calculate(this.Cart, customer)
                };
            }

            return this.View(viewModel);
        }

        [HttpPost]
        [Route("Index")]
        public async Task<IActionResult> Index(int id, Uri returnUrl)
        {
            var product = await this.productService.GetProductAsync(id);

            if (product != null && this.ModelState.IsValid)
            {
                this.Cart.AddItem(product, 1);

                var customer = new CustomerDTO();

                return this.View(new CartViewModel
                {
                    Cart = this.Cart,
                    ReturnUrl = returnUrl,
                    Pricing = this.priceService.Calculate(this.Cart, customer)
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

                var customer = new CustomerDTO();

                model = new CartViewModel
                {
                    Cart = this.Cart,
                    ReturnUrl = returnUrl,
                    Pricing = this.priceService.Calculate(this.Cart, customer)
                };
            }

            return this.View("Index", model);
        }
    }
}
