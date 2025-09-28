using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LyfegenContracts.Application.Common.Validation
{
    public static class ValidationHelpers
    {
        public static async Task EnsureValidAsync<T>(IValidator<T> validator, T instance)
        {
            var result = await validator.ValidateAsync(instance);
            if (!result.IsValid) throw new ValidationException(result.Errors);
        }
    }
}
