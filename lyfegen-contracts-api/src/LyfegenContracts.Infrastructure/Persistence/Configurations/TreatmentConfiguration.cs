using LyfegenContracts.Domain.Domain.Treatments;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Infrastructure.Persistence.Configurations
{
    public class TreatmentConfiguration : IEntityTypeConfiguration<Treatment>
    {
        public void Configure(EntityTypeBuilder<Treatment> b)
        {
            b.ToTable("Treatments");
            b.HasKey(x => x.Id);

            b.Property(x => x.PatientId).IsRequired();
            b.Property(x => x.ContractId).IsRequired();
            b.Property(x => x.ProductPackId).IsRequired();
            b.Property(x => x.StartDateUtc).IsRequired();

            b.HasOne(x => x.Contract)
             .WithMany(x => x.Treatments)
             .HasForeignKey(x => x.ContractId);

            b.HasOne(x => x.Patient)
             .WithMany(x => x.Treatments)
             .HasForeignKey(x => x.PatientId);

            b.HasOne(x => x.ProductPack)
             .WithMany()
             .HasForeignKey(x => x.ProductPackId);
        }
    }
}
