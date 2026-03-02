using GamerStore.Data.Repository;
using GamerStore.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GamerStore.Controllers
{
    [Route("shop")]
    public class ShopController : Controller
    {
        private readonly IStoreRepository repository;

        public ShopController(IStoreRepository repository)
        {
            this.repository = repository;
        }

        private int PageSize { get; } = 8;

        [HttpGet("all")]
        public ViewResult Index(string[] brands, string? sort, string? searchText, string? category, int productPage = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel();

            if (this.ModelState.IsValid)
            {
                var listOfProducts = this.repository.Products;
                if (!listOfProducts.Any())
                {
                    return this.View(model);
                }

                var products = this.repository.Products
                .Where(p => (category == null || p.Category.Name == category) && (brands == null || brands.Length == 0 || brands.Contains(p.Brand.Name)))
                .OrderBy(p => p.Id)
                .Skip((productPage - 1) * this.PageSize)
                .Take(this.PageSize);

                products = sort switch
                {
                    "name_asc" => products
                    .OrderBy(p => p.Brand.Name)
                    .ThenBy(p => p.Model)
                    .ThenBy(p => p.Title),
                    "price_asc" => products.OrderBy(p => p.Price),
                    "price_desc" => products.OrderByDescending(p => p.Price),
                    _ => products
                };

                products = products.Where(p => string.IsNullOrEmpty(searchText) ||
                (p.Brand.Name + " " + p.Model + " " + p.Title).ToLower()!.Contains(searchText.ToLower()));

                int totalItems = this.repository.Products
                .Where(p => (category == null || p.Category.Name == category) &&
                (brands == null || brands.Length == 0 || brands.Contains(p.Brand.Name)))
                .Count();

                model = new ProductsListViewModel
                {
                    Products = products,
                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = this.PageSize,
                        TotalItems = totalItems,
                    },
                    CurrentCategory = category,
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
