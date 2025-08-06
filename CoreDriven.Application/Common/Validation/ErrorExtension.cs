using ErrorOr;
using FluentValidation.Results;

namespace CoreDriven.Application.Common.Validation;

public static class ErrorExtension
{
    public static List<Error> GetErrors(this IEnumerable<ValidationFailure> errors) =>
        errors
            .Select(e => Error.Validation(code: e.PropertyName, description: e.ErrorMessage))
            .ToList(); 
}