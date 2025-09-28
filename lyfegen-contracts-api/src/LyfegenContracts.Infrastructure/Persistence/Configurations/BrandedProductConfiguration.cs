using LyfegenContracts.Domain.Domain.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Infrastructure.Persistence.Configurations
{
    public class BrandedProductConfiguration : IEntityTypeConfiguration<BrandedProduct>
    {
        public void Configure(EntityTypeBuilder<BrandedProduct> b)
        {
            b.ToTable("BrandedProducts");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
        }
    }
}
