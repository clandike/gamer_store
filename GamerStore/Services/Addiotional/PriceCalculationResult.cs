namespace GamerStore.Services.Addiotional
{
    public class PriceCalculationResult
    {
        public decimal BasePrice { get; set; }
        public decimal FinalPrice { get; set; }

        public List<AppliedDiscount> AppliedDiscounts { get; set; } = new();
    }

    public class AppliedDiscount
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
    }
}
