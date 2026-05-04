using GamerStore.Models.Data;
using GamerStore.Models.DTO;
using GamerStore.Models.ViewModels;

namespace GamerStore.Services
{
    public interface IPromotionsService
    {
        Task<PromotionsViewModel> GetPromoAsync();
    }

    public class PromotionsService : IPromotionsService
    {
        private readonly IProductService productService;

        public PromotionsService(IProductService productService)
        {
            this.productService = productService;
        }

        public async Task<PromotionsViewModel> GetPromoAsync()
        {
            var products = await this.productService.GetProductsAsync();
            var categories = await this.productService.GetCategoriesAsync();

            var result = new PromotionsViewModel
            {
                TimePromos = Models.Discounts.PromoData.PromoProductTimeBased.Select(p => new TimePromoVM
                {
                    DiscountPercentage = (int)p.DiscountPercentage,
                    EndDate = p.EndDate,
                    ProductName = this.GetProductName(products?.SingleOrDefault(x => x.Id == p.ItemId)!),
                    Progress = GetProgress(p.StartDate, p.EndDate),
                }),

                CategoryPromos = Models.Discounts.PromoData.PromoCategoryBased.Select(p => new CategoryPromoVM
                {
                    DiscountPercentage = (int)p.DiscountPercentage,
                    CategoryName = categories.SingleOrDefault(x => x.Id == p.ItemId).Name,
                })
            };

            return result;
        }

        private string GetProductName(ProductDTO product)
        {
            return product != null ? string.Join(" ", product.BrandName, product.Model, product.Title) : "Unknown Product";
        }

        private int GetProgress(DateTime startDate, DateTime endDate)
        {
            var progressRaw = (DateTime.Now - startDate).TotalSeconds / (endDate - startDate).TotalSeconds * 100;

            return (int)Math.Clamp(progressRaw, 0, 100);
        }
    }
}
