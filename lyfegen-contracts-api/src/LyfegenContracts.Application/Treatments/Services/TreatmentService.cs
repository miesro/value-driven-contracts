using FluentValidation;
using FluentValidation.Results;
using LyfegenContracts.Application.Common.Validation;
using LyfegenContracts.Application.Contracts.Dtos;
using LyfegenContracts.Application.Contracts.Repositories;
using LyfegenContracts.Application.Patients.Repositories;
using LyfegenContracts.Application.Products.Repositories;
using LyfegenContracts.Application.Treatments.Dtos;
using LyfegenContracts.Application.Treatments.Repositories;
using LyfegenContracts.Domain.Domain.Treatments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Treatments.Services
{
    public class TreatmentService : ITreatmentService
    {
        //TODO: Use some service that injects DateTime.UtcNow for easier testing

        private readonly ITreatmentRepository _treatments;
        private readonly IPatientRepository _patients;
        private readonly IProductRepository _products;
        private readonly IContractRepository _contracts;
        private readonly IValidator<CreateTreatmentDto> _createValidator;
        private readonly IValidator<SetTreatmentOutcomeDto> _outcomeValidator;

        public TreatmentService(
            ITreatmentRepository treatments,
            IPatientRepository patients,
            IProductRepository products,
            IContractRepository contracts,
            IValidator<CreateTreatmentDto> createValidator,
            IValidator<SetTreatmentOutcomeDto> outcomeValidator)
        {
            _treatments = treatments;
            _patients = patients;
            _products = products;
            _contracts = contracts;
            _createValidator = createValidator;
            _outcomeValidator = outcomeValidator;
        }

        public async Task<TreatmentDto> CreateAsync(CreateTreatmentDto request)
        {
            await ValidationHelpers.EnsureValidAsync(_createValidator, request);

            var failures = new List<ValidationFailure>();

            var patient = await _patients.GetByIdAsync(request.PatientId);
            if(patient == null)
                failures.Add(new ValidationFailure(nameof(CreateTreatmentDto.PatientId), "Patient not found."));

            var pack = await _products.GetProductPackByIdAsync(request.ProductPackId);
            if(pack == null)
                failures.Add(new ValidationFailure(nameof(CreateTreatmentDto.ProductPackId), "Product Pack not found."));

            var contract = await _contracts.GetByIdAsync(request.ContractId);
            if(contract == null)
                failures.Add(new ValidationFailure(nameof(CreateTreatmentDto.ContractId), "Contract not found."));

            if (pack.MedicinalProduct.BrandedProductId != contract.BrandedProductId)
                failures.Add(new ValidationFailure(nameof(CreateTreatmentDto.ContractId), 
                    "Selected product pack is not part of the contract’s branded product."));

            var ageAtStart = GetAgeInYears(patient.BirthDate, request.StartDateUtc);
            if (!(patient.CancerStage >= contract.MinStage &&
                  patient.CancerStage <= contract.MaxStage &&
                  ageAtStart < contract.MaxAgeExclusive))
                failures.Add(new ValidationFailure(nameof(CreateTreatmentDto.PatientId),
                    "Patient does not satisfy contract enrollment criteria."));

            if (failures.Count > 0)
                throw new ValidationException(failures);

            var entity = new Treatment
            {
                PatientId = request.PatientId,
                ProductPackId = request.ProductPackId,
                StartDateUtc = request.StartDateUtc,
                DurationMonths = request.DurationMonths,
                ContractId = request.ContractId
            };

            var saved = await _treatments.AddAsync(entity);
            var reloaded = await _treatments.GetByIdAsync(saved.Id);
            return ToDetailsDto(reloaded);
        }

        public async Task<TreatmentDto> SetOutcomeAsync(long treatmentId, SetTreatmentOutcomeDto request)
        {
            await ValidationHelpers.EnsureValidAsync(_outcomeValidator, request);

            var treatment = await _treatments.GetByIdAsync(treatmentId)
                ?? throw new InvalidOperationException("Treatment not found");

            if (treatment.TreatmentOutcome != null)
                throw new InvalidOperationException("Outcome already settled");

            var pack = treatment.ProductPack
                ?? throw new InvalidOperationException("Product pack not loaded.");

            var contract = treatment.Contract
                ?? throw new InvalidOperationException("Contract not loaded.");

            var basePrice = pack.BasePriceChf;

            int appliedRate;

            var osBoundary = treatment.StartDateUtc.AddMonths(contract.OSMonths);
            var pfsBoundary = treatment.StartDateUtc.AddMonths(contract.PFSMonths);

            bool progressedByPfsBoundary = request.ProgressionDateUtc.HasValue 
                && request.ProgressionDateUtc.Value <= pfsBoundary;

            //progression happened within the PFS window -> OS pricing
            if (progressedByPfsBoundary)
            {
                bool aliveAtOsBoundary = !request.DeathDateUtc.HasValue 
                    || request.DeathDateUtc.Value >= osBoundary;
                appliedRate = aliveAtOsBoundary? contract.OsAfterMonthsRate : contract.OsBeforeMonthsRate;
            }
            else// no progression within PFS window -> PFS pricing
            {
                bool aliveAtPfsBoundary = !request.DeathDateUtc.HasValue 
                    || request.DeathDateUtc.Value >= pfsBoundary;
                appliedRate = aliveAtPfsBoundary ? contract.PfsAfterMonthsRate
                    : contract.PfsBeforeMonthsRate;
            }

            var payable = Math.Round(basePrice * appliedRate / 100m, 2, MidpointRounding.AwayFromZero);
            var refund = Math.Round(basePrice - payable, 2, MidpointRounding.AwayFromZero);

            treatment.TreatmentOutcome = new TreatmentOutcome
            {
                TreatmentId = treatment.Id,
                ProgressionDateUtc = request.ProgressionDateUtc,
                DeathDateUtc = request.DeathDateUtc,
                PaymentRate = appliedRate,
                PayableAmountChf = payable,
                RefundAmountChf = refund,
                EffectiveDate = DateOnly.FromDateTime(DateTime.Now)
            };

            await _treatments.UpdateAsync(treatment);

            var updated = await _treatments.GetByIdAsync(treatment.Id) ?? treatment;
            return ToDetailsDto(updated);
        }

        private static TreatmentDto ToDetailsDto(Treatment t)
            => new TreatmentDto
            {
                Id = t.Id,
                PatientId = t.PatientId,
                ProductPackId = t.ProductPackId,
                StartDateUtc = t.StartDateUtc,
                DurationMonths = t.DurationMonths,
                ContractId = t.ContractId,
                Outcome = t.TreatmentOutcome == null
                    ? null
                    : new TreatmentOutcomeDto
                    {
                        TreatmentId = t.TreatmentOutcome.TreatmentId,
                        ProgressionDateUtc = t.TreatmentOutcome.ProgressionDateUtc,
                        DeathDateUtc = t.TreatmentOutcome.DeathDateUtc,
                        PaymentRate = t.TreatmentOutcome.PaymentRate,
                        PayableAmountChf = t.TreatmentOutcome.PayableAmountChf,
                        RefundAmountChf = t.TreatmentOutcome.RefundAmountChf,
                        EffectiveDate = t.TreatmentOutcome.EffectiveDate
                    }
            };

        private int GetAgeInYears(DateOnly birthDate, DateOnly atDate)
        {
            var age = atDate.Year - birthDate.Year;
            if (atDate.Month < birthDate.Month || (atDate.Month == birthDate.Month && atDate.Day < birthDate.Day))
                age--;
            return age;
        }
    }
}
