using FluentValidation;

namespace Domain.Validations.Validators;

/// <summary>
/// Валидация номера телефона
/// </summary>
public class PhoneValidator : AbstractValidator<string>
{
    public PhoneValidator(string paramName)
    {
        RuleFor(p => p)
            .NotNull().WithMessage(string.Format(ErrorMessages.NullError, paramName))
            .NotEmpty().WithMessage(string.Format(ErrorMessages.EmptyError, paramName))
            .Matches(RegexPatterns.PhonePattern).WithMessage(ErrorMessages.PhoneFormat);
    }
    
}