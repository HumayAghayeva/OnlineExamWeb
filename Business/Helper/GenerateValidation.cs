using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Business.Helper
{
    public static class GenerateValidation
    {
        public static ValidationResult GenerateValidationResult(bool isValid, string message, List<ValidationFailure> failures)
        {
            var result = new ValidationResult(failures ?? new List<ValidationFailure>());

            if (!isValid)
            {
                result.Errors.Add(new ValidationFailure("Error", message));
            }

            return result;
        }

    }
}
