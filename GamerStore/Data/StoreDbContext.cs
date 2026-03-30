using GamerStore.Models;
using GamerStore.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace GamerStore.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products => this.Set<Product>();

        public DbSet<Brand> Brands => this.Set<Brand>();

        public DbSet<Category> Categories => this.Set<Category>();

        public DbSet<ProductImage> ProductImages => this.Set<ProductImage>();

        public DbSet<Order> Orders => this.Set<Order>();

        public DbSet<OrderInfo> OrderInfos => this.Set<OrderInfo>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property<string>(p => p.ShortDescription)
                .HasMaxLength(500);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10,2)");

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ProductImage>()
                .HasOne(i => i.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderInfo>()
                .HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderInfos)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
