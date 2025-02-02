using FluentValidation;
using TestApp.Web.Domain.Abstraction.Models;

namespace TestApp.WebApi.Validators;

public sealed class PatientModelValidator : AbstractValidator<PatientModel>
{
    public PatientModelValidator()
    {
        RuleFor(p => p.Name)
            .NotNull()
            .SetValidator(new PatientModelNameValidator());

        RuleFor(p => p.Gender)
            .IsInEnum().WithMessage("Invalid gender. Allowed values: 'male', 'female', 'other', 'unknown'.");

        RuleFor(p => p.BirthDate)
            .NotNull();

        RuleFor(p => p.Active)
            .NotNull();
    }
}