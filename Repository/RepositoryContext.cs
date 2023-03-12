using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Configuration;


namespace Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryAttributes>()
           .Property(e => e.Value)
           .HasConversion(
               v => JsonConvert.SerializeObject(v),
               v => JsonConvert.DeserializeObject<object>(v));

            modelBuilder.Entity<CategoryProduct>()
            .HasKey(o => new { o.CategoryId, o.ProductId });

            modelBuilder.Entity<CategoryAttributes>()
            .HasKey(o => new { o.CategoryId, o.AttributeId });


            modelBuilder.ApplyConfiguration(new CategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryProductsConfiguration());
            modelBuilder.ApplyConfiguration(new AttributeTypesConfiguration());
            modelBuilder.ApplyConfiguration(new AttributesConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryAttributesConfiguration());

        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<CategoryProduct>? CategoryProducts { get; set; }

    }
}