using GamerStore.Models.Discounts;
using GamerStore.Models.DTO;

namespace GamerStore.Models
{
    public class Cart
    {
        private readonly List<CartLine> lines = new List<CartLine>();

        public IReadOnlyList<CartLine> Lines
        {
            get { return this.lines; }
        }

        public virtual void AddItem(ProductDTO product, int quantity)
        {
            CartLine? line = this.lines.Find(p => p.Product.Id == product.Id);

            if (line is null)
            {
                List<Promo> appliedPromo = new List<Promo>();

                var timePromo = PromoData.PromoProductTimeBaseds.FirstOrDefault(x => x.Id == product.Id);
                if (timePromo is not null)
                {
                    appliedPromo.Add(timePromo);
                }

                var categoryPromo = PromoData.PromoCategories.FirstOrDefault(x => x.Id == product.CategoryId);
                if (categoryPromo is not null)
                {
                    appliedPromo.Add(categoryPromo);
                }

                this.lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                    AppliedPromo = appliedPromo,
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(ProductDTO product) => this.lines.RemoveAll(l => l.Product.Id == product.Id);

        public decimal ComputeTotalValue() => this.lines.Sum(e => e.Product.Price * e.Quantity);

        public decimal ComputeTotalValueWithLinePromos()
        {
            decimal total = 0m;

            foreach (var line in this.lines)
            {
                var price = line.Product.Price;

                if (line.AppliedPromo != null && line.AppliedPromo.Any())
                {
                    var promo = line.AppliedPromo.First();
                    price = price * (1 - promo.DiscountPercentage / 100m);
                }

                total += price * line.Quantity;
            }

            return total;
        }

        public virtual void Clear() => this.lines.Clear();
    }
}
