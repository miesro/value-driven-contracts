using LyfegenContracts.Domain.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Contracts.Repositories
{
    public interface IContractRepository
    {
        Task<Contract> AddAsync(Contract entity);
        Task<Contract?> GetByIdAsync(long id);
        Task<IReadOnlyList<Contract>> GetAllAsync();
        Task<IReadOnlyList<Contract>> GetMatchingAsync(
            long brandedProductId, int cancerStage, int patientAge);
    }
}
