using LyfegenContracts.Application.Contracts.Repositories;
using LyfegenContracts.Domain.Domain.Contracts;
using LyfegenContracts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Infrastructure.Repositories
{
    public class ContractRepository : IContractRepository
    {
        private readonly AppDbContext _db;

        public ContractRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Contract> AddAsync(Contract entity)
        {
            _db.Contracts.Add(entity);
            await _db.SaveChangesAsync();//TODO: Do not save here in repo but implement unit of work pattern            
            return entity;
        }

        public async Task<Contract?> GetByIdAsync(long id)
        {
            return await _db.Contracts
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IReadOnlyList<Contract>> GetAllAsync()
        {
            return await _db.Contracts
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Contract>> GetMatchingAsync(
            long brandedProductId, int cancerStage, int patientAge)
        {
            return await _db.Contracts
                .Where(c =>
                    c.BrandedProductId == brandedProductId &&
                    cancerStage >= c.MinStage &&
                    cancerStage <= c.MaxStage &&
                    patientAge < c.MaxAgeExclusive)
                .ToListAsync();
        }
    }
}
