using FluentValidation;
using TestApp.Web.Domain.Abstraction.Models;

namespace TestApp.WebApi.Validators;

public sealed class PatientModelNameValidator : AbstractValidator<PatientNameModel>
{
    public PatientModelNameValidator()
    {
        RuleFor(p => p.Use)
            .MaximumLength(300);

        RuleFor(p => p.Family)
            .NotEmpty()
            .MaximumLength(30);

        RuleFor(p => p.Given)
            .NotNull()
            .ForEach(p => p.NotEmpty().MaximumLength(30));

        RuleFor(p => p.Given.Length)
            .GreaterThanOrEqualTo(2)
            .LessThanOrEqualTo(2);

    }
}