using Domain.Validations.Primitives;
using FluentValidation;

namespace Domain.Validations.Validators;

public class TelegramValidator : AbstractValidator<string>
{
    public TelegramValidator(string paramName)
    {
        RuleFor(t => t)
            .NotNull().WithMessage(string.Format(ErrorMessages.NullError, paramName))
            .NotEmpty().WithMessage(string.Format(ErrorMessages.EmptyError, paramName))
            .Matches(RegexPatterns.TelegramPattern).WithMessage(ErrorMessages.TelegramFormat);
    }
}