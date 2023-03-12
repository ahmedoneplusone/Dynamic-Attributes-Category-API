using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class CategoryProductsConfiguration : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.HasData
            (
                new CategoryProduct
                {
                    CategoryId = 2,
                    ProductId = 1
                }, 
                new CategoryProduct
                {
                    CategoryId = 3,
                    ProductId = 2
                }
            );
        }
    }
}
