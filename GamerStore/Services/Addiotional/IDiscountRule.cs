using GamerStore.Models;
using GamerStore.Models.DTO;
using Newtonsoft.Json.Linq;

namespace GamerStore.Services.Addiotional
{
    /// <summary>
    /// Specifies the available discount rules that can be applied to a transaction.
    /// </summary>
    /// <remarks>Use this enumeration to indicate which type of discount logic should be used when processing
    /// a transaction. The values correspond to different discount strategies, such as loyalty-based or bulk purchase
    /// discounts.</remarks>
    public enum DiscountRule
    {
        /// <summary>
        /// Indicates that no options or flags are set.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that the loyalty program is selected as the reward type.
        /// </summary>
        Loyalty = 1,

        /// <summary>
        /// Indicates that the operation or item is processed in bulk, typically involving multiple entities or actions
        /// at once.
        /// </summary>
        Bulk = 2,
    }

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
            { Loyalty.Bronze, 8 },
            { Loyalty.Silver, 15 },
            { Loyalty.Gold, 23 },
        };

        public string Name => "Loyalty Discount";

        public DiscountRule DiscountRule => DiscountRule.Loyalty;

        public bool IsApplicable(Cart cart, CustomerDTO user)
        {
            return loyalties.TryGetValue(user.LoyaltyLevel, out decimal value);
        }

        public decimal CalculateDiscount(Cart cart, CustomerDTO user)
        {
            loyalties.TryGetValue(user.LoyaltyLevel, out decimal value);
            var total = cart.ComputeTotalValueWithLinePromos();

            return total * (value / 100m);
        }
    }

    public class BulkDiscountRule : IDiscountRule
    {
        public string Name => "Bulk Discount";

        public DiscountRule DiscountRule => DiscountRule.Bulk;

        public bool IsApplicable(Cart cart, CustomerDTO user)
        {
            return cart.Lines.Sum(x => x.Quantity) >= 3;
        }

        public decimal CalculateDiscount(Cart cart, CustomerDTO user)
        {
            var total = cart.ComputeTotalValueWithLinePromos();
            var value = 9;

            return total * (value / 100m);
        }
    }
}
