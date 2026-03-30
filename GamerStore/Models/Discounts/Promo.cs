namespace GamerStore.Models.Discounts
{
    public class Promo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal DiscountPercentage { get; set; }
    }

    public class PromoProductTimeBased : Promo
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class PromoCategory : Promo
    {
    }

    public static class PromoData
    {
        public static IEnumerable<PromoProductTimeBased> PromoProductTimeBaseds { get; set; } = new List<PromoProductTimeBased>()
        {
            new PromoProductTimeBased { Id = 1, DiscountPercentage = 5, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(2) },
            new PromoProductTimeBased { Id = 10, DiscountPercentage = 7, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(2) },
        };

        public static IEnumerable<PromoCategory> PromoCategories { get; set; } = new List<PromoCategory>()
        {
            new PromoCategory { Id = 2, DiscountPercentage = 10 },
            new PromoCategory { Id = 3, DiscountPercentage = 15 },
        };
    }
}
