using FluentValidation;
using LyfegenContracts.Application.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Contracts.Validation
{
    public class MatchContractsRequestDtoValidator : AbstractValidator<MatchContractsRequestDto>
    {
        public MatchContractsRequestDtoValidator()
        {
            RuleFor(x => x.ProductPackId).GreaterThan(0);
            RuleFor(x => x.CancerStage).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PatientAge).GreaterThanOrEqualTo(0);
        }
    }
}
