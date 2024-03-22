using Domain.Validations.Primitives;
using FluentValidation;

namespace Domain.Validations.Validators;

public class BirthDateValidator : AbstractValidator<DateTime>
{
    public BirthDateValidator(string paramName)
    {
        RuleFor(d => d)
            .NotNull().WithMessage(string.Format(ErrorMessages.NullError, paramName))
            .NotEmpty().WithMessage(string.Format(ErrorMessages.EmptyError, paramName))
            .LessThan(DateTime.Now).WithMessage(string.Format(ErrorMessages.FutureDate, paramName))
            .GreaterThan(DateTime.Now.AddYears(-150)).WithMessage(string.Format(ErrorMessages.OldDate, paramName));
    }
}