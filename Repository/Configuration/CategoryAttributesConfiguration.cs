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
    public class CategoryAttributesConfiguration : IEntityTypeConfiguration<CategoryAttributes>
    {
        public void Configure(EntityTypeBuilder<CategoryAttributes> builder)
        {
            builder.HasData
            (
                new CategoryAttributes
                {
                    CategoryId = 2,
                    AttributeId = 1,
                    Value = "55.5\""
                },
                new CategoryAttributes
                {
                    CategoryId = 2,
                    AttributeId = 2,
                    Value = 10
                },
                new CategoryAttributes
                {
                    CategoryId = 3,
                    AttributeId = 1,
                    Value = "6.7\""
                },
                new CategoryAttributes
                {
                    CategoryId = 3,
                    AttributeId = 2,
                    Value = 1
                }
            );
        }
    }
}
