using GamerStore.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GamerStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IStoreRepository repostory;

        public NavigationMenuViewComponent(IStoreRepository repostory)
        {
            this.repostory = repostory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            this.ViewBag.SelectedCategory = this.HttpContext.Request.Query["category"];
            this.ViewBag.SelectedBrands = this.HttpContext.Request.Query["brands"];
            this.ViewBag.SelectedSort = this.HttpContext.Request.Query["sort"];
            this.ViewBag.SearchText = this.HttpContext.Request.Query["searchText"];

            var categories = await this.repostory.GetCategoriesAsync();
            var brands = await this.repostory.GetBrandsAsync();

            return this.View((categories.Select(x => x.Name)
                .OrderBy(x => x).AsEnumerable(), brands.Select(x => x.Name).OrderBy(x => x).AsEnumerable()));
        }
    }
}
