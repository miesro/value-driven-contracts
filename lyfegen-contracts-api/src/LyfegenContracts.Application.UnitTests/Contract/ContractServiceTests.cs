using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using LyfegenContracts.Application.Contracts.Dtos;
using LyfegenContracts.Application.Contracts.Repositories;
using LyfegenContracts.Application.Contracts.Services;
using LyfegenContracts.Application.Parties;
using LyfegenContracts.Application.Products.Repositories;
using Moq;
using Xunit;

namespace LyfegenContracts.Application.UnitTests.Contract
{
    public class ContractServiceTests
    {
        private readonly Mock<IContractRepository> _contracts = new();
        private readonly Mock<IProductRepository> _products = new();
        private readonly Mock<IPartyRepository> _parties = new();
        private readonly Mock<IValidator<CreateContractDto>> _createValidator = new();
        private readonly Mock<IValidator<MatchContractsRequestDto>> _matchValidator = new();

        private ContractService CreateSut()
            => new ContractService(
                _contracts.Object,
                _products.Object,
                _parties.Object,
                _createValidator.Object,
                _matchValidator.Object);

        private CreateContractDto ValidCreateDto() => new CreateContractDto
        {
            PayerPartyId = 10,
            ManufacturerPartyId = 20,
            BrandedProductId = 30,
            OsMonths = 12,
            PfsMonths = 9,
            OsAfterMonthsRate = 75,
            OsBeforeMonthsRate = 30,
            PfsAfterMonthsRate = 85,
            PfsBeforeMonthsRate = 40,
            MinStage = 0,
            MaxStage = 3,
            MaxAgeExclusive = 55
        };

        [Fact]
        public async Task CreateAsync_ValidInputData_MapsAndSaves()
        {
            // Arrange
            var dto = ValidCreateDto();

            _createValidator.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());
            _products.Setup(p => p.BrandedProductExistsAsync(dto.BrandedProductId))
                .ReturnsAsync(true);
            _parties.Setup(p => p.PayerExistsAsync(dto.PayerPartyId))
                .ReturnsAsync(true);
            _parties.Setup(p => p.ManufacturerExistsAsync(dto.ManufacturerPartyId))
                .ReturnsAsync(true);

            _contracts.Setup(r => r.AddAsync(It.IsAny<Domain.Domain.Contracts.Contract>()))
                .ReturnsAsync((Domain.Domain.Contracts.Contract c) =>
                {
                    c.Id = 999;
                    return c;
                });

            var sut = CreateSut();

            // Act
            var result = await sut.CreateAsync(dto);

            // Assert
            Assert.Equal(999, result.Id);
            Assert.Equal(dto.PayerPartyId, result.PayerPartyId);
            Assert.Equal(dto.ManufacturerPartyId, result.ManufacturerPartyId);
            Assert.Equal(dto.BrandedProductId, result.BrandedProductId);
            Assert.Equal(dto.OsMonths, result.OsMonths);
            Assert.Equal(dto.PfsMonths, result.PfsMonths);
            Assert.Equal(dto.OsAfterMonthsRate, result.OsAfterMonthsRate);
            Assert.Equal(dto.OsBeforeMonthsRate, result.OsBeforeMonthsRate);
            Assert.Equal(dto.PfsAfterMonthsRate, result.PfsAfterMonthsRate);
            Assert.Equal(dto.PfsBeforeMonthsRate, result.PfsBeforeMonthsRate);
            Assert.Equal(dto.MinStage, result.MinStage);
            Assert.Equal(dto.MaxStage, result.MaxStage);
            Assert.Equal(dto.MaxAgeExclusive, result.MaxAgeExclusive);

            // verify it was called once with proper mapping
            _contracts.Verify(r => r.AddAsync(It.Is<Domain.Domain.Contracts.Contract>(c =>
                c.PayerId == dto.PayerPartyId &&
                c.ManufacturerId == dto.ManufacturerPartyId &&
                c.BrandedProductId == dto.BrandedProductId &&
                c.OSMonths == dto.OsMonths &&
                c.PFSMonths == dto.PfsMonths &&
                c.OsAfterMonthsRate == dto.OsAfterMonthsRate &&
                c.OsBeforeMonthsRate == dto.OsBeforeMonthsRate &&
                c.PfsAfterMonthsRate == dto.PfsAfterMonthsRate &&
                c.PfsBeforeMonthsRate == dto.PfsBeforeMonthsRate &&
                c.MinStage == dto.MinStage &&
                c.MaxStage == dto.MaxStage &&
                c.MaxAgeExclusive == dto.MaxAgeExclusive
            )), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_MissingReferences_ThrowsValidationExceptionWithFieldErrors()
        {
            // Arrange
            var dto = ValidCreateDto();
            _createValidator.Setup(v => v.ValidateAsync(dto, default)).ReturnsAsync(new ValidationResult());

            _products.Setup(p => p.BrandedProductExistsAsync(dto.BrandedProductId))
                .ReturnsAsync(false);
            _parties.Setup(p => p.PayerExistsAsync(dto.PayerPartyId))
                .ReturnsAsync(false);
            _parties.Setup(p => p.ManufacturerExistsAsync(dto.ManufacturerPartyId))
                .ReturnsAsync(false);

            var sut = CreateSut();

            // Act
            var ex = await Assert.ThrowsAsync<ValidationException>(() => sut.CreateAsync(dto));
            var errors = ex.Errors.ToList();

            // Assert
            Assert.Contains(errors, e => e.PropertyName == nameof(CreateContractDto.BrandedProductId));
            Assert.Contains(errors, e => e.PropertyName == nameof(CreateContractDto.PayerPartyId));
            Assert.Contains(errors, e => e.PropertyName == nameof(CreateContractDto.ManufacturerPartyId));

            // assert AddAsync was never called
            _contracts.Verify(r => r.AddAsync(It.IsAny<Domain.Domain.Contracts.Contract>()), Times.Never);
        }
    }
}
