using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LyfegenContracts.Domain.Domain.Contracts;

namespace LyfegenContracts.Infrastructure.Persistence.Configurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> b)
        {
            b.ToTable("Contracts");
            b.HasKey(x => x.Id);

            b.Property(x => x.ManufacturerId).IsRequired();
            b.Property(x => x.PayerId).IsRequired();
            b.Property(x => x.BrandedProductId).IsRequired();

            b.Property(x => x.MinStage).IsRequired();
            b.Property(x => x.MaxStage).IsRequired();
            b.Property(x => x.MaxAgeExclusive).IsRequired();
            b.Property(x => x.OSMonths).IsRequired();
            b.Property(x => x.PFSMonths).IsRequired();

            b.Property(x => x.OsAfterMonthsRate).IsRequired();
            b.Property(x => x.OsBeforeMonthsRate).IsRequired();
            b.Property(x => x.PfsAfterMonthsRate).IsRequired();
            b.Property(x => x.PfsBeforeMonthsRate).IsRequired();

            b.HasOne(x => x.Manufacturer)
             .WithMany()
             .HasForeignKey(x => x.ManufacturerId);

            b.HasOne(x => x.Payer)
             .WithMany()
             .HasForeignKey(x => x.PayerId);

            b.HasOne(x => x.BrandedProduct)
             .WithMany()
             .HasForeignKey(x => x.BrandedProductId);
        }
    }
}
