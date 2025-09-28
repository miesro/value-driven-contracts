using LyfegenContracts.Domain.Domain.Contracts;
using LyfegenContracts.Domain.Domain.Parties;
using LyfegenContracts.Domain.Domain.Patients;
using LyfegenContracts.Domain.Domain.Products;
using LyfegenContracts.Domain.Domain.Treatments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contract> Contracts => Set<Contract>();
        public DbSet<Payer> Payers => Set<Payer>();
        public DbSet<Manufacturer> Manufacturers => Set<Manufacturer>();
        public DbSet<Patient> Patients => Set<Patient>();
        public DbSet<Treatment> Treatments => Set<Treatment>();
        public DbSet<TreatmentOutcome> TreatmentOutcomes => Set<TreatmentOutcome>();
        public DbSet<BrandedProduct> BrandedProducts => Set<BrandedProduct>();
        public DbSet<MedicinalProduct> MedicinalProducts => Set<MedicinalProduct>();
        public DbSet<ProductPack> ProductPacks => Set<ProductPack>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Configurations.PayerConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ManufacturerConfiguration());

            modelBuilder.ApplyConfiguration(new Configurations.BrandedProductConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.MedicinalProductConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.ProductPackConfiguration());

            modelBuilder.ApplyConfiguration(new Configurations.PatientConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.TreatmentConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.TreatmentOutcomeConfiguration());

            modelBuilder.ApplyConfiguration(new Configurations.ContractConfiguration());
        }
    }
}
