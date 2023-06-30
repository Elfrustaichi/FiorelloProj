using Fiorello.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fiorello.Data.Configurations
{
    public class FlowerConfiguration : IEntityTypeConfiguration<Flower>
    {
        public void Configure(EntityTypeBuilder<Flower> builder)
        {
            builder.Property(x=>x.Name).IsRequired(true).HasMaxLength(50);
            builder.Property(x=>x.Description).IsRequired(true).HasMaxLength(500);
            builder.Property(x => x.CostPrice).HasColumnType("money");
            builder.Property(x => x.SalePrice).HasColumnType("money");
        }
    }
}
