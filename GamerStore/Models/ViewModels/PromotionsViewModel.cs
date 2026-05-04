namespace GamerStore.Models.ViewModels
{
    public class PromotionsViewModel
    {
        public IEnumerable<TimePromoVM> TimePromos { get; set; }

        public IEnumerable<CategoryPromoVM> CategoryPromos { get; set; }
    }

    public class TimePromoVM
    {
        public int DiscountPercentage { get; set; }

        public DateTime EndDate { get; set; }

        public string ProductName { get; set; }

        public int Progress { get; set; }
    }

    public class CategoryPromoVM
    {
        public int DiscountPercentage { get; set; }

        public string CategoryName { get; set; }
    }
}
