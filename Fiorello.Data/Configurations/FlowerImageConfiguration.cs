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
    public class FlowerImageConfiguration : IEntityTypeConfiguration<FlowerImage>
    {
        public void Configure(EntityTypeBuilder<FlowerImage> builder)
        {
           builder.Property(x=>x.ImageUrl).IsRequired(true).HasMaxLength(100);
            builder.Property(x => x.PosterStatus).IsRequired(true);


        }
    }
}
