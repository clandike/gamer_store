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
        Task<ProductDTO> GetProductAsync(int productId);

        Task<PageInfo> GetPaginatedProductsAsync(string[] brands, string? sort, string? searchText, string? category, int pageSize, int productPage);
        
        Task<List<ProductDTO>> GetProductsAsync();
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
            Product? product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new ProductDTO();
            }

            return new ProductDTO()
            {
                Id = product.Id,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                Model = product.Model,
                Title = product.Title,
                ShortDescription = product.ShortDescription,
                FullDescription = product.FullDescription,
                Price = product.Price,
                ImageFileName = product.ImageFileName,
                IsInStock = product.IsInStock,
            };
        }

        public async Task<List<ProductDTO>> GetProductsAsync()
        {
            var products = await this.repository.GetProductsAsync();
            return products.Select(p => new ProductDTO()
            {
                Id = p.Id,
                BrandId = p.BrandId,
                CategoryId = p.CategoryId,
                Model = p.Model,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                FullDescription = p.FullDescription,
                Price = p.Price,
                ImageFileName = p.ImageFileName,
                IsInStock = p.IsInStock,
            }).ToList();
        }

        public async Task<PageInfo> GetPaginatedProductsAsync(string[] brands, string? sort, string? searchText, string? category, int pageSize, int productPage)
        {
            var listOfProducts = await this.repository.GetProductsAsync();
            if (!listOfProducts.Any())
            {
                return new PageInfo();
            }

            var products = listOfProducts
            .Where(p => (category == null || p.Category.Name == category) && (brands == null || brands.Length == 0 || brands.Contains(p.Brand.Name)))
            .OrderBy(p => p.Id)
            .Skip((productPage - 1) * pageSize)
            .Take(pageSize);

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
            (p.Brand.Name + " " + p.Model + " " + p.Title + " " + p.Category).ToLower()!.Contains(searchText.ToLower()));

            int totalItems = products
            .Count();

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
    }
}
