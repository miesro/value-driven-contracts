using FluentValidation;
using FluentValidation.Results;
using LyfegenContracts.Application.Common.Validation;
using LyfegenContracts.Application.Contracts.Dtos;
using LyfegenContracts.Application.Contracts.Repositories;
using LyfegenContracts.Application.Parties;
using LyfegenContracts.Application.Products.Repositories;
using LyfegenContracts.Domain.Domain.Contracts;

namespace LyfegenContracts.Application.Contracts.Services
{
    public class ContractService : IContractService
    {
        private readonly IContractRepository _contracts;
        private readonly IProductRepository _products;
        private readonly IPartyRepository _parties;
        private readonly IValidator<CreateContractDto> _createValidator;
        private readonly IValidator<MatchContractsRequestDto> _matchValidator;

        public ContractService(
            IContractRepository contracts,
            IProductRepository products,
            IPartyRepository parties,
            IValidator<CreateContractDto> createValidator,
            IValidator<MatchContractsRequestDto> matchValidator)
        {
            _contracts = contracts;
            _products = products;
            _parties = parties;
            _createValidator = createValidator;
            _matchValidator = matchValidator;
        }

        public async Task<ContractDto> CreateAsync(CreateContractDto request)
        {
            await ValidationHelpers.EnsureValidAsync(_createValidator, request);

            var failures = new List<ValidationFailure>();

            if (!await _products.BrandedProductExistsAsync(request.BrandedProductId))
                failures.Add(new ValidationFailure(nameof(CreateContractDto.BrandedProductId), "Branded product not found."));

            if (!await _parties.PayerExistsAsync(request.PayerPartyId))
                failures.Add(new ValidationFailure(nameof(CreateContractDto.PayerPartyId), "Payer not found."));

            if (!await _parties.ManufacturerExistsAsync(request.ManufacturerPartyId))
                failures.Add(new ValidationFailure(nameof(CreateContractDto.ManufacturerPartyId), "Manufacturer not found."));

            if (failures.Count > 0)
                throw new ValidationException(failures);

            var entity = new Contract
            {
                BrandedProductId = request.BrandedProductId,
                OSMonths = request.OsMonths,
                PFSMonths = request.PfsMonths,
                OsAfterMonthsRate = request.OsAfterMonthsRate,
                OsBeforeMonthsRate = request.OsBeforeMonthsRate,
                PfsAfterMonthsRate = request.PfsAfterMonthsRate,
                PfsBeforeMonthsRate = request.PfsBeforeMonthsRate,
                MinStage = request.MinStage,
                MaxStage = request.MaxStage,
                MaxAgeExclusive = request.MaxAgeExclusive,
                ManufacturerId = request.ManufacturerPartyId,
                PayerId = request.PayerPartyId
            };

            var saved = await _contracts.AddAsync(entity);
            return Map(saved);
        }

        public async Task<IReadOnlyList<ContractDto>> GetAllAsync()
        {
            var items = await _contracts.GetAllAsync();
            return items.Select(Map).ToList();
        }

        public async Task<IReadOnlyList<ContractDto>> GetMatchingAsync(MatchContractsRequestDto request)
        {
            await ValidationHelpers.EnsureValidAsync(_matchValidator, request);
            
            var brandedProduct = await _products.GetBrandedProductByPackIdAsync(
                request.ProductPackId);

            if(brandedProduct == null)
                throw new ValidationException(new[]
                {
                    new ValidationFailure(nameof(MatchContractsRequestDto.ProductPackId), "Product pack not found.")
                });

            var items = await _contracts.GetMatchingAsync(
                brandedProduct.Id, request.CancerStage, request.PatientAge);
            return items.Select(Map).ToList();
        }

        private ContractDto Map(Contract c) => new ContractDto
        {
            Id = c.Id,
            PayerPartyId = c.PayerId,
            ManufacturerPartyId = c.ManufacturerId,
            BrandedProductId = c.BrandedProductId,
            OsMonths = c.OSMonths,
            PfsMonths = c.PFSMonths,
            OsAfterMonthsRate = c.OsAfterMonthsRate,
            OsBeforeMonthsRate = c.OsBeforeMonthsRate,
            PfsAfterMonthsRate = c.PfsAfterMonthsRate,
            PfsBeforeMonthsRate = c.PfsBeforeMonthsRate,
            MinStage = c.MinStage,
            MaxStage = c.MaxStage,
            MaxAgeExclusive = c.MaxAgeExclusive
        };
    }
}
