using LyfegenContracts.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Contracts.Services
{
    public interface IContractService
    {
        Task<ContractDto> CreateAsync(CreateContractDto request);
        Task<IReadOnlyList<ContractDto>> GetAllAsync();
        Task<IReadOnlyList<ContractDto>> GetMatchingAsync(MatchContractsRequestDto request);
    }
}
