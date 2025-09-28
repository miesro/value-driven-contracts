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
    public class MedicinalProductConfiguration : IEntityTypeConfiguration<MedicinalProduct>
    {
        public void Configure(EntityTypeBuilder<MedicinalProduct> b)
        {
            b.ToTable("MedicinalProducts");
            b.HasKey(x => x.Id);

            b.Property(x => x.DisplayName).HasMaxLength(200).IsRequired();
            b.Property(x => x.StrengthMgPerMl)
             .HasColumnType("numeric(10,2)")
             .IsRequired();

            b.Property(x => x.BrandedProductId).IsRequired();

            b.HasOne(x => x.BrandedProduct)
                .WithMany(x => x.MedicinalProducts)
                .HasForeignKey(x => x.BrandedProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
