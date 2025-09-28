using LyfegenContracts.Application.Parties;
using LyfegenContracts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Infrastructure.Repositories
{
    public class PartyRepository : IPartyRepository
    {
        private readonly AppDbContext _db;
        public PartyRepository(AppDbContext db) => _db = db;

        public async Task<bool> PayerExistsAsync(long payerId)
        {
            return await _db.Payers.AnyAsync(p => p.Id == payerId);
        }

        public async Task<bool> ManufacturerExistsAsync(long manufacturerId)
        {
            return await _db.Manufacturers.AnyAsync(m => m.Id == manufacturerId);
        }
    }
}
