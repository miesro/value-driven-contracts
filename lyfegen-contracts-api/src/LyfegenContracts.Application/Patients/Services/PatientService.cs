using FluentValidation;
using LyfegenContracts.Application.Common.Validation;
using LyfegenContracts.Application.Contracts.Dtos;
using LyfegenContracts.Application.Patients.Dtos;
using LyfegenContracts.Application.Patients.Repositories;
using LyfegenContracts.Application.Treatments.Dtos;
using LyfegenContracts.Domain.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Patients.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _repo;
        private readonly IValidator<CreatePatientDto> _createValidator;

        public PatientService(IPatientRepository repo, IValidator<CreatePatientDto> createValidator)
        {
            _repo = repo;
            _createValidator = createValidator;
        }

        public async Task<PatientDetailsDto> CreateAsync(CreatePatientDto request)
        {
            await ValidationHelpers.EnsureValidAsync(_createValidator, request);

            var patient = new Patient
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                BirthDate = request.BirthDate,
                CancerStage = request.CancerStage
            };

            var saved = await _repo.AddAsync(patient);

            return new PatientDetailsDto
            {
                Id = saved.Id,
                FirstName = saved.FirstName,
                LastName = saved.LastName,
                BirthDate = saved.BirthDate,
                CancerStage = saved.CancerStage,
                Treatments = Array.Empty<TreatmentDto>()
            };
        }

        public async Task<IReadOnlyList<PatientListItemDto>> GetAllAsync()
        {
            var patients = await _repo.GetAllAsync();
            return patients
                .Select(p => new PatientListItemDto
                {
                    Id = p.Id,
                    Name = $"{p.FirstName} {p.LastName}".Trim()
                })
                .ToList();
        }

        public async Task<PatientDetailsDto?> GetByIdAsync(long id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity is null) return null;

            var treatments = entity.Treatments?
                .Select(t => new TreatmentDto
                {
                    Id = t.Id,
                    PatientId = t.PatientId,
                    ContractId = t.ContractId,
                    ProductPackId = t.ProductPackId,
                    StartDateUtc = t.StartDateUtc,
                    DurationMonths = t.DurationMonths,
                    Outcome = t.TreatmentOutcome is null ? null : new TreatmentOutcomeDto
                    {
                        TreatmentId = t.TreatmentOutcome.TreatmentId,
                        ProgressionDateUtc = t.TreatmentOutcome.ProgressionDateUtc,
                        DeathDateUtc = t.TreatmentOutcome.DeathDateUtc,
                        PaymentRate = t.TreatmentOutcome.PaymentRate,
                        PayableAmountChf = t.TreatmentOutcome.PayableAmountChf,
                        RefundAmountChf = t.TreatmentOutcome.RefundAmountChf,
                        EffectiveDate = t.TreatmentOutcome.EffectiveDate
                    }
                })
                .ToList() ?? new List<TreatmentDto>();

            return new PatientDetailsDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                BirthDate = entity.BirthDate,
                CancerStage = entity.CancerStage,
                Treatments = treatments
            };
        }
    }
}
