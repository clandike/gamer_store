using GamerStore.Models;

namespace GamerStore.Data.Repository
{
    public interface IStoreRepository
    {
        IQueryable<Product> Products { get; }

        IQueryable<Brand> Brands { get; }

        IQueryable<Category> Categories { get; }

        void SaveProduct(Product product);

        void CreateProduct(Product product);

        void DeleteProduct(Product product);
    }
}
