using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attribute = Entities.Models.Attribute;

namespace Repository.Configuration
{
    public class AttributesConfiguration : IEntityTypeConfiguration<Attribute>
    {
        public void Configure(EntityTypeBuilder<Attribute> builder)
        {
            builder.HasData
            (
                new Attribute
                {
                    Id = 1,
                    Name = "Screen Size",
                    TypeId = 1
                }, 
                new Attribute
                {
                    Id = 2,
                    Name = "Weight",
                    TypeId = 2
                }
            );
        }
    }
}
