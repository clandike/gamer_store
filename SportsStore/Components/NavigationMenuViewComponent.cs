using Microsoft.AspNetCore.Mvc;
using GamerStore.Data.Repository;

namespace GamerStore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private readonly IStoreRepository repostory;

        public NavigationMenuViewComponent(IStoreRepository repostory)
        {
            this.repostory = repostory;
        }

        public IViewComponentResult Invoke()
        {
            this.ViewBag.SelectedCategory = this.HttpContext.Request.Query["category"];
            this.ViewBag.SelectedBrands = this.HttpContext.Request.Query["brands"];
            this.ViewBag.SelectedSort = this.HttpContext.Request.Query["sort"];
            this.ViewBag.SearchText = this.HttpContext.Request.Query["searchText"];

            return this.View((this.repostory.Categories.Select(x => x.Name)
                .OrderBy(x => x).AsEnumerable(), this.repostory.Brands.Select(x => x.Name).OrderBy(x => x).AsEnumerable()));
        }
    }
}
