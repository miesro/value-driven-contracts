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
    public class ProductPackConfiguration : IEntityTypeConfiguration<ProductPack>
    {
        public void Configure(EntityTypeBuilder<ProductPack> b)
        {
            b.ToTable("ProductPacks");
            b.HasKey(x => x.Id);

            b.Property(x => x.MedicinalProductId).IsRequired();
            b.Property(x => x.Units).IsRequired();
            b.Property(x => x.BasePriceChf)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            b.HasOne(x => x.MedicinalProduct)
                .WithMany(x => x.ProductPacks)
                .HasForeignKey(x => x.MedicinalProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
