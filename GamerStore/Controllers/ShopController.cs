using GamerStore.Models.ViewModels;
using GamerStore.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamerStore.Controllers
{
    [Route("shop")]
    public class ShopController : Controller
    {
        private readonly IProductService productService;

        public ShopController(IProductService productService)
        {
            this.productService = productService;
        }

        private int PageSize { get; } = 8;

        [HttpGet("all")]
        public async Task<ViewResult> Index(string[] brands, string? sort, string? searchText, string? category, int productPage = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel();

            if (this.ModelState.IsValid)
            {
                var pageInfo = await this.productService.GetPaginatedProductsAsync(brands, sort, searchText, category, this.PageSize, productPage);

                model = new ProductsListViewModel
                {
                    Products = pageInfo.Products,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = this.PageSize,
                        TotalItems = pageInfo.SortedProductCount,
                    },
                    CurrentCategory = category,
                    SelectedBrands = brands,
                    SearchText = searchText,
                    Sort = sort,
                };
            }

            return this.View(model);
        }

        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View();
        }
    }
}
