using LyfegenContracts.Application.Treatments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Treatments.Services
{
    public interface ITreatmentService
    {
        Task<TreatmentDto> CreateAsync(CreateTreatmentDto request);
        Task<TreatmentDto> SetOutcomeAsync(long treatmentId, SetTreatmentOutcomeDto request);
    }
}
