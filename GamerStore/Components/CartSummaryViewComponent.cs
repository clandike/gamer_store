using Microsoft.AspNetCore.Mvc;
using GamerStore.Models;

namespace GamerStore.Components
{
    public class CartSummaryViewComponent : ViewComponent
    {
        private readonly Cart cart;

        public CartSummaryViewComponent(Cart cart)
        {
            this.cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return this.View(this.cart);
        }
    }
}
