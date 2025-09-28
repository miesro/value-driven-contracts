using LyfegenContracts.Application.Treatments.Repositories;
using LyfegenContracts.Domain.Domain.Treatments;
using LyfegenContracts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Infrastructure.Repositories
{
    public class TreatmentRepository : ITreatmentRepository
    {
        private readonly AppDbContext _db;

        public TreatmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Treatment> AddAsync(Treatment entity)
        {
            _db.Treatments.Add(entity);
            await _db.SaveChangesAsync();//TODO: save in unitofwork
            return entity;
        }

        public async Task<Treatment?> GetByIdAsync(long id)
        {
            return await _db.Treatments
                .Include(t => t.ProductPack)
                    .ThenInclude(pp => pp.MedicinalProduct)
                        .ThenInclude(mp => mp.BrandedProduct)
                .Include(t => t.Contract)
                .Include(t => t.TreatmentOutcome)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task UpdateAsync(Treatment entity)
        {
            _db.Treatments.Update(entity);
            await _db.SaveChangesAsync();//TODO: save in unitofwork
        }
    }
}
