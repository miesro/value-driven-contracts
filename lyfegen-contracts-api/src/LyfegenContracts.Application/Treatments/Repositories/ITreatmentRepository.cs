using LyfegenContracts.Domain.Domain.Treatments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Treatments.Repositories
{
    public interface ITreatmentRepository
    {
        Task<Treatment> AddAsync(Treatment entity);
        Task<Treatment?> GetByIdAsync(long id);
        Task UpdateAsync(Treatment entity);
    }
}
