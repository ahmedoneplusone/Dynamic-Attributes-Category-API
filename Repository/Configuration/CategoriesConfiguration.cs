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
    public class CategoriesConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category
                {
                    Id = 1,
                    Name = "Electronics",
                    ParentCategoryId = null
                }, 
                new Category 
                {
                    Id = 2,
                    Name = "T.Vs",
                    ParentCategoryId = 1
                },
                new Category
                {
                    Id = 3,
                    Name = "Phones",
                    ParentCategoryId = 1
                }
            );
        }
    }
}
