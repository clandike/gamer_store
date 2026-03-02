using Microsoft.EntityFrameworkCore;
using GamerStore.Models;

namespace GamerStore.Data.Repository
{
    public class EFStoreRepository : IStoreRepository
    {
        private readonly StoreDbContext context;

        public EFStoreRepository(StoreDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Product> Products => this.context.Products
            .Include(x => x.Category)
            .Include(x => x.Brand);

        public IQueryable<Category> Categories => this.context.Categories;

        public IQueryable<Brand> Brands => context.Brands;

        public void CreateProduct(Product product)
        {
            context.Add(product);
            context.SaveChanges();
        }

        public void DeleteProduct(Product product)
        {
            context.Remove(product);
            context.SaveChanges();
        }

        public void SaveProduct(Product product)
        {
            if (product.Id == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                var dbEntry = context.Products?.FirstOrDefault(p => p.Id == product.Id);

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

            context.SaveChanges();
        }
    }
}
