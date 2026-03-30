using GamerStore.Services.Addiotional;

namespace GamerStore.Models.ViewModels
{
    public class CartViewModel
    {
        public Cart? Cart { get; set; } = new();

        public Uri? ReturnUrl { get; set; } = new Uri("/", UriKind.Relative);

        public PriceCalculationResult? Pricing { get; set; }

    }
}
