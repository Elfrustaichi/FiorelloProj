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
    public class SliderConfiguration : IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.Property(x=>x.Title).IsRequired(true).HasMaxLength(20);
            builder.Property(x=>x.Description).HasMaxLength(500);
            builder.Property(x=>x.SignatureImageUrl).IsRequired(true).HasMaxLength(100);
            builder.Property(x=>x.BackgroundImageUrl).IsRequired(true).HasMaxLength(100);
        }
    }
}
