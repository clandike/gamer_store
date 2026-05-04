namespace GamerStore.Services.Addiotional
{
    public class PriceCalculationResult
    {
        public decimal OriginalPrice { get; set; }

        public decimal PriceWithPromos { get; set; }

        public decimal FinalPrice { get; set; }

        public List<DiscountResult> AppliedDiscounts { get; set; } = new();
    }

    public class DiscountResult
    {
        public string Name { get; set; }

        public decimal Amount { get; set; }
    }
}
