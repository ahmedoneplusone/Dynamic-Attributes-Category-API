using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class AttributeTypesConfiguration : IEntityTypeConfiguration<AttributeType>
    {
        public void Configure(EntityTypeBuilder<AttributeType> builder)
        {
            builder.HasData
            (
                new AttributeType
                {
                    Id = 1,
                    Type = typeof(string).ToString()
                },
                new AttributeType
                {
                    Id = 2,
                    Type = typeof(int).ToString()
                },
                new AttributeType
                {
                    Id = 3,
                    Type = typeof(DateTime).ToString()
                },
                new AttributeType
                {
                    Id = 4,
                    Type = typeof(byte).ToString()
                }
            );
        }
    }
}
