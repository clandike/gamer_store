using Microsoft.EntityFrameworkCore;
using GamerStore.Models.Data;

namespace GamerStore.Data.Repository
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;

        public EFStoreRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Product>> GetProductsAsync() => await this.context.Products
            .Include(x => x.Category)
            .Include(x => x.Brand).ToListAsync();

        public async Task<List<Category>> GetCategoriesAsync() => await this.context.Categories.ToListAsync();

        public async Task<List<Brand>> GetBrandsAsync() => await this.context.Brands.ToListAsync();

        public async Task CreateProductAsync(Product product)
        {
            await this.context.AddAsync(product);
            await this.context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            this.context.Remove(product);
            await this.context.SaveChangesAsync();
        }

        public async Task SaveProductAsync(Product product)
        {
            if (product.Id == 0)
            {
                await this.context.AddAsync(product);
            }
            else
            {
                var dbEntry = this.context.Products?.FirstOrDefault(p => p.Id == product.Id);

                if (dbEntry != null)
                {
                    dbEntry.BrandId = product.BrandId;
                    dbEntry.CategoryId = product.CategoryId;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageFileName = product.ImageFileName;
                    dbEntry.ShortDescription = product.ShortDescription;
                    dbEntry.Title = product.Title;
                    dbEntry.Model = product.Model;
                }
            }

            await this.context.SaveChangesAsync();
        }
    }
}
