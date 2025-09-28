using LyfegenContracts.Application.Patients.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Patients.Services
{
    public interface IPatientService
    {
        Task<PatientDetailsDto> CreateAsync(CreatePatientDto request);
        Task<IReadOnlyList<PatientListItemDto>> GetAllAsync();
        Task<PatientDetailsDto?> GetByIdAsync(long id);
    }
}
