using LyfegenContracts.Domain.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Patients.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient> AddAsync(Patient entity);
        Task<Patient?> GetByIdAsync(long id);
        Task<IReadOnlyList<Patient>> GetAllAsync();
    }
}
