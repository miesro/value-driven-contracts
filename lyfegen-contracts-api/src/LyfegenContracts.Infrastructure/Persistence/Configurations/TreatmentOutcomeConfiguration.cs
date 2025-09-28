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
    public class TreatmentOutcomeConfiguration : IEntityTypeConfiguration<TreatmentOutcome>
    {
        public void Configure(EntityTypeBuilder<TreatmentOutcome> b)
        {
            b.ToTable("TreatmentOutcomes");
            b.HasKey(x => x.TreatmentId);

            b.Property(x => x.ProgressionDateUtc);
            b.Property(x => x.DeathDateUtc);
            b.Property(x => x.EffectiveDate);

            b.Property(x => x.PaymentRate)
                .IsRequired();

            b.Property(x => x.PayableAmountChf)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            b.Property(x => x.RefundAmountChf)
                .HasColumnType("numeric(18,2)")
                .IsRequired();

            b.HasOne(o => o.Treatment)
                .WithOne(t => t.TreatmentOutcome)
                .HasForeignKey<TreatmentOutcome>(o => o.TreatmentId);
        }
    }
}
