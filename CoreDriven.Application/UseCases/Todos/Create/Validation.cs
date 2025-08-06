using FluentValidation;

namespace CoreDriven.Application.UseCases.Todos.Create;

public class Validation: AbstractValidator<Request>
{
    public Validation()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}