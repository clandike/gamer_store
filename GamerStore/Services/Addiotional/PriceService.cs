using GamerStore.Models;
using GamerStore.Models.DTO;

namespace GamerStore.Services.Addiotional
{
    public interface IPriceService
    {
        PriceCalculationResult Calculate(Cart cart, CustomerDTO user);
    }

    public class PriceService: IPriceService
    {
        private readonly IEnumerable<IDiscountRule> _rules;

        public PriceService(IEnumerable<IDiscountRule> rules)
        {
            _rules = rules;
        }

        public PriceCalculationResult Calculate(Cart cart, CustomerDTO user)
        {
            var result = new PriceCalculationResult
            {
                BasePrice = cart.ComputeTotalValue(),
            };

            decimal totalDiscount = 0;

            foreach (var rule in _rules)
            {
                if (!rule.IsApplicable(cart, user))
                    continue;

                var discount = rule.CalculateDiscount(cart, user);

                totalDiscount += discount;

                result.AppliedDiscounts.Add(new AppliedDiscount
                {
                    Name = rule.Name,
                    Amount = discount
                });
            }

            result.FinalPrice = cart.ComputeTotalValue() - totalDiscount;

            return result;
        }
    }
}
