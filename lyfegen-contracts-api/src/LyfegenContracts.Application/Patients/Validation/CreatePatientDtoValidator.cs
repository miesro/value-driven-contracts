using FluentValidation;
using LyfegenContracts.Application.Patients.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Patients.Validation
{
    public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientDtoValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.BirthDate)
                .NotEmpty();

            RuleFor(x => x.CancerStage)
                .GreaterThanOrEqualTo(0);
        }
    }
}
