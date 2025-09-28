using LyfegenContracts.Application.Patients.Repositories;
using LyfegenContracts.Domain.Domain.Patients;
using LyfegenContracts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Infrastructure.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly AppDbContext _db;

        public PatientRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Patient> AddAsync(Patient entity)
        {
            _db.Patients.Add(entity);
            await _db.SaveChangesAsync();//TODO: Do not save here in repo but implement unit of work pattern
            return entity;
        }

        public async Task<Patient?> GetByIdAsync(long id)
        {
            return await _db.Patients
                .Include(p => p.Treatments)
                    .ThenInclude(t => t.TreatmentOutcome)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Patient>> GetAllAsync()
        {
            return await _db.Patients
                .OrderBy(p => p.LastName).ThenBy(p => p.FirstName)
                .ToListAsync();
        }
    }
}
