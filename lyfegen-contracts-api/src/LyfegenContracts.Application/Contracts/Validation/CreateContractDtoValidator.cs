using FluentValidation;
using LyfegenContracts.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Contracts.Validation
{
    public class CreateContractDtoValidator : AbstractValidator<CreateContractDto>
    {
        public CreateContractDtoValidator()
        {
            RuleFor(x => x.PayerPartyId).GreaterThan(0);
            RuleFor(x => x.ManufacturerPartyId).GreaterThan(0);
            RuleFor(x => x.BrandedProductId).GreaterThan(0);
            RuleFor(x => x.OsMonths).GreaterThan(0);
            RuleFor(x => x.PfsMonths).GreaterThan(0);

            RuleFor(x => x.OsAfterMonthsRate).InclusiveBetween(0, 100);
            RuleFor(x => x.OsBeforeMonthsRate).InclusiveBetween(0, 100);
            RuleFor(x => x.PfsAfterMonthsRate).InclusiveBetween(0, 100);
            RuleFor(x => x.PfsBeforeMonthsRate).InclusiveBetween(0, 100);

            RuleFor(x => x.MinStage).GreaterThanOrEqualTo(0);

            RuleFor(x => x.MaxAgeExclusive).GreaterThan(0);

            RuleFor(x => x.PfsMonths)
                .LessThanOrEqualTo(x => x.OsMonths);
        }
    }
}
