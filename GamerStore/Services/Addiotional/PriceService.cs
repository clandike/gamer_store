using GamerStore.Models;
using GamerStore.Models.Discounts;
using GamerStore.Models.DTO;

namespace GamerStore.Services.Addiotional
{
    public interface IPriceService
    {
        PriceCalculationResult Calculate(Cart cart, CustomerDTO user);
    }

    public class PriceService : IPriceService
    {
        private readonly IEnumerable<IDiscountRule> rules;

        public PriceService(IEnumerable<IDiscountRule> rules)
        {
            this.rules = rules;
        }

        public PriceCalculationResult Calculate(Cart cart, CustomerDTO user)
        {
            var total = cart.ComputeTotalValue();
            var totalWithPromos = cart.ComputeTotalValueWithLinePromos();

            var test = this.rules.ToList();

            var appliedRules = this.rules.ToList()
                .Where(r => r.IsApplicable(cart, user))
                .ToList();

            var discounts = appliedRules
                .Select(r => new DiscountResult
                {
                    Name = r.Name,
                    Amount = r.CalculateDiscount(cart, user),
                })
                .ToList();

            var totalDiscountPercentage = discounts.Sum(d => d.Amount);

            return new PriceCalculationResult
            {
                OriginalPrice = total,
                FinalPrice = totalWithPromos - totalDiscountPercentage,
                AppliedDiscounts = discounts,
            };
        }
    }
}
