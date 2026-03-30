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
    }
}
