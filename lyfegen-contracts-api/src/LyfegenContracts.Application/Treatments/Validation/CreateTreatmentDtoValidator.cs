using FluentValidation;
using LyfegenContracts.Application.Treatments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Treatments.Validation
{
    public class CreateTreatmentDtoValidator : AbstractValidator<CreateTreatmentDto>
    {
        public CreateTreatmentDtoValidator()
        {
            RuleFor(x => x.PatientId).GreaterThan(0);
            RuleFor(x => x.ProductPackId).GreaterThan(0);
            RuleFor(x => x.ContractId).GreaterThan(0);
            RuleFor(x => x.StartDateUtc).NotEmpty();
            RuleFor(x => x.DurationMonths).GreaterThan(0);
        }
    }
}
