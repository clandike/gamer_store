using GamerStore.Data.Repository;
using GamerStore.Models;
using GamerStore.Models.Data;
using GamerStore.Models.DTO;

namespace GamerStore.Services
{
    public interface IStoreService
    {
        Task<Order> GetInformation(int productId);

        Task<PageInfo> GetPaginatedOrdersAsync(string[] brands, string? sort, string? searchText, string? category, int pageSize, int productPage);

        Task<List<Order>> GetOrdersAsync();
    }

    public class StoreService : IStoreService
    {
        private readonly IStoreRepository repository;

        public StoreService(IStoreRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Order> GetInformation(int productId)
        {
            await repository.GetBrandsAsync();
            return new Order();
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
            (p.Brand.Name + " " + p.Model + " " + p.Title).ToLower()!.Contains(searchText.ToLower()));

            int totalItems = listOfProducts
            .Where(p => (category == null || p.Category.Name == category) &&
            (brands == null || brands.Length == 0 || brands.Contains(p.Brand.Name)))
            .Count();

            return new PageInfo()
            {
                Products = products.Select(p => new ProductDTO()
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
                }).ToList(),

                SortedProductCount = totalItems,
            };
        }

        public Task<PageInfo> GetPaginatedOrdersAsync(string[] brands, string? sort, string? searchText, string? category, int pageSize, int productPage)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrdersAsync()
        {
            throw new NotImplementedException();
        }
    }
}
