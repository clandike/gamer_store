using GamerStore.Data.Repository;
using GamerStore.Models.Data;
using GamerStore.Models.DTO;

namespace GamerStore.Services
{
    public class PageInfo
    {
        public List<ProductDTO> Products { get; set; }

        public int SortedProductCount { get; set; }
    }

    public interface IProductService
    {
        Task<CategoryDTO> GetCategoryByIdAsync(int id);

        Task<ProductDTO> GetProductAsync(int productId);

        Task<PageInfo> GetPaginatedProductsAsync(string[] brands, string? sort, string? searchText, string? category, int pageSize, int productPage);

        Task<List<ProductDTO>> GetProductsAsync();

        Task<List<CategoryDTO>> GetCategoriesAsync();
    }

    public class ProductService : IProductService
    {
        private readonly IStoreRepository repository;

        public ProductService(IStoreRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProductDTO> GetProductAsync(int id)
        {
            var products = await this.repository.GetProductsAsync();
            var brands = await this.repository.GetBrandsAsync();
            var categories = await this.repository.GetCategoriesAsync();

            Product? product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new ProductDTO();
            }

            return new ProductDTO()
            {
                Id = product.Id,
                BrandId = product.BrandId,
                BrandName = brands.FirstOrDefault(b => b.Id == product.BrandId)?.Name ?? string.Empty,
                CategoryId = product.CategoryId,
                Model = product.Model,
                Title = product.Title,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                Price = product.Price,
                ImageFileName = product.ImageFileName,
                IsInStock = product.IsInStock,
                CategoryName = categories.FirstOrDefault(c => c.Id == product.CategoryId)?.Name ?? string.Empty,
            };
        }

        public async Task<List<ProductDTO>> GetProductsAsync()
        {
            var products = await this.repository.GetProductsAsync();
            return products.Select(p => new ProductDTO()
            {
                Id = p.Id,
                BrandId = p.BrandId,
                BrandName = p.Brand?.Name,
                CategoryId = p.CategoryId,
                Model = p.Model,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                FullDescription = p.FullDescription,
                Price = p.Price,
                ImageFileName = p.ImageFileName,
                IsInStock = p.IsInStock,
                CategoryName = p.Category?.Name,
            }).ToList();
        }

        public async Task<PageInfo> GetPaginatedProductsAsync(
    string[] brands,
    string? sort,
    string? searchText,
    string? category,
    int pageSize,
    int productPage)
        {
            var listOfProducts = await this.repository.GetProductsAsync();

            if (!listOfProducts.Any())
                return new PageInfo();

            var query = listOfProducts.AsQueryable();

            // 🔹 Нормалізація вхідних даних
            var normalizedCategory = category?.Trim().ToLower();
            var normalizedBrands = brands?
                .Where(b => !string.IsNullOrWhiteSpace(b))
                .Select(b => b.Trim().ToLower())
                .ToArray();

            // 🔹 Фільтрація (category + brands разом працюють коректно)
            query = query.Where(p =>
                (normalizedCategory == null || p.Category.Name.ToLower() == normalizedCategory) &&
                (normalizedBrands == null || normalizedBrands.Length == 0 ||
                 normalizedBrands.Contains(p.Brand.Name.ToLower()))
            );

            // 🔹 Пошук
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var lowerSearch = searchText.Trim().ToLower();

                query = query.Where(p =>
                    (p.Brand.Name + " " + p.Model + " " + p.Title + " " + p.Category.Name)
                    .ToLower()
                    .Contains(lowerSearch)
                );
            }

            // 🔹 Сортування (ВАЖЛИВО: завжди стабільне)
            query = sort switch
            {
                "name_asc" => query
                    .OrderBy(p => p.Brand.Name)
                    .ThenBy(p => p.Model)
                    .ThenBy(p => p.Title)
                    .ThenBy(p => p.Id), // 🔥 стабілізатор

                "price_asc" => query
                    .OrderBy(p => p.Price)
                    .ThenBy(p => p.Id), // 🔥 стабілізатор

                "price_desc" => query
                    .OrderByDescending(p => p.Price)
                    .ThenBy(p => p.Id), // 🔥 стабілізатор

                _ => query.OrderBy(p => p.Id)
            };

            // 🔹 Загальна кількість
            int totalItems = query.Count();

            // 🔹 Пагінація (тільки після ВСЬОГО)
            var products = query
                .Skip((productPage - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PageInfo()
            {
                Products = products.Select(p => new ProductDTO()
                {
                    Id = p.Id,
                    BrandId = p.BrandId,
                    BrandName = p.Brand.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Model = p.Model,
                    Title = p.Title,
                    ShortDescription = p.ShortDescription,
                    FullDescription = p.FullDescription,
                    Price = p.Price,
                    ImageFileName = p.ImageFileName,
                    IsInStock = p.IsInStock,
                }).ToList(),

                SortedProductCount = totalItems,
            };
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
        {
            var categories = await repository.GetCategoriesAsync();
            var category = categories.FirstOrDefault(c => c.Id == id);

            return new CategoryDTO()
            {
                Id = category?.Id ?? 0,
                Name = category?.Name ?? string.Empty,
            };
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            var categories = await repository.GetCategoriesAsync();
            return categories.Select(c => new CategoryDTO()
            {
                Id = c.Id,
                Name = c.Name,
            }).ToList();
        }
    }
}
