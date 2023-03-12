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
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData
            (
                new Product
                {
                    Id = 1,
                    Name = "Sony 55 inch OLED",
                    Price = 16788,
                    Quantity = 5
                }, 
                new Product
                {
                    Id = 2,
                    Name = "Iphone 14 PRO",
                    Price = 45500,
                    Quantity = 20
                }
            );
        }
    }
}
