using GamerStore.Models;
using GamerStore.Models.DTO;

namespace GamerStore.Services.Addiotional
{
    public interface IDiscountRule
    {
        bool IsApplicable(Cart cart, CustomerDTO user);

        decimal CalculateDiscount(Cart cart, CustomerDTO user);

        string Name { get; }
    }

    public class LoyaltyDiscountRule : IDiscountRule
    {
        private Dictionary<Loyalty, decimal> loyalties = new Dictionary<Loyalty, decimal>()
        {
            { Loyalty.Bronze, 0.1m },
            { Loyalty.Silver, 0.18m },
            { Loyalty.Gold, 0.3m },
        };

        public string Name => "Loyalty Discount";

        public bool IsApplicable(Cart cart, CustomerDTO user)
        {
            return loyalties.TryGetValue(user.LoyaltyLevel, out decimal value);
        }

        public decimal CalculateDiscount(Cart cart, CustomerDTO user)
        {
            loyalties.TryGetValue(user.LoyaltyLevel, out decimal value);

            return cart.ComputeTotalValue() * value; // 10%
        }
    }

    public class BulkDiscountRule : IDiscountRule
    {
        public string Name => "Bulk Discount";

        public bool IsApplicable(Cart cart, CustomerDTO user)
        {
            return cart.ComputeTotalValue() >= 1000 || cart.Lines.Count >= 10;
        }

        public decimal CalculateDiscount(Cart cart, CustomerDTO user)
        {
            return cart.ComputeTotalValue() * 0.09m;
        }
    }
}
