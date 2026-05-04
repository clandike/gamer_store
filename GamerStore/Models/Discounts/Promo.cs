using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GamerStore.Models.Discounts
{
    public class Promo
    {
        [Key]
        [BindNever]
        public int Id { get; set; }

        public int ItemId { get; set; }

        public string Name { get; set; }

        public decimal DiscountPercentage { get; set; }
    }

    public class PromoProductTimeBased : Promo
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }

    public class PromoCategoryBased : Promo
    {
    }

    public static class PromoData
    {
        public static IEnumerable<PromoProductTimeBased> PromoProductTimeBased { get; set; } = new List<PromoProductTimeBased>()
        {
            new()
            {
                ItemId = 7,
                DiscountPercentage = 5,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2),
            },
            new ()
            {
                ItemId = 10,
                DiscountPercentage = 7,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2),
            },
        };

        public static IEnumerable<PromoCategoryBased> PromoCategoryBased { get; set; } = new List<PromoCategoryBased>()
        {
            new ()
            {
                ItemId = 2,
                DiscountPercentage = 10,
            },
            new ()
            {
                ItemId = 3,
                DiscountPercentage = 15,
            },
        };
    }
}
