using GamerStore.Models.Discounts;
using GamerStore.Models.DTO;

namespace GamerStore.Models
{
    public class CartLine
    {
        public int CartLineId { get; set; }

        public ProductDTO Product { get; set; } = new();

        public int Quantity { get; set; }

        public List<Promo>? AppliedPromo { get; set; }

        public decimal GetDiscountedPrice()
        {
            var price = Product.Price;

            if (AppliedPromo != null && AppliedPromo.Any())
            {
                foreach (var promo in AppliedPromo)
                {
                    price *= (1 - promo.DiscountPercentage / 100m);
                }
            }

            return price;
        }

        public decimal GetTotalPrice()
        {
            return GetDiscountedPrice() * Quantity;
        }
    }
}
