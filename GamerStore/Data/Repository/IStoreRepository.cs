using GamerStore.Models.Data;

namespace GamerStore.Data.Repository
{
    public interface IStoreRepository
    {
        Task<List<Product>> GetProductsAsync();

        Task<List<Brand>> GetBrandsAsync();

        Task<List<Category>> GetCategoriesAsync();

        Task SaveProductAsync(Product product);

        Task CreateProductAsync(Product product);

        Task DeleteProductAsync(Product product);
    }
}
