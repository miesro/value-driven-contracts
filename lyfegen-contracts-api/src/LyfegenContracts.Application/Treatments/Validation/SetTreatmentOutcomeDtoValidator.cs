using FluentValidation;
using LyfegenContracts.Application.Treatments.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Treatments.Validation
{
    public class SetTreatmentOutcomeDtoValidator : AbstractValidator<SetTreatmentOutcomeDto>
    {
        public SetTreatmentOutcomeDtoValidator()
        {
            RuleFor(x => x)
                .Must(x => x.ProgressionDateUtc.HasValue || x.DeathDateUtc.HasValue);
        }
    }
}
