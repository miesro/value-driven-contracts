using LyfegenContracts.Domain.Domain.Parties;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Infrastructure.Persistence.Configurations
{
    public class PayerConfiguration : IEntityTypeConfiguration<Payer>
    {
        public void Configure(EntityTypeBuilder<Payer> b)
        {
            b.ToTable("Payers");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).HasMaxLength(200).IsRequired();
        }
    }
}
