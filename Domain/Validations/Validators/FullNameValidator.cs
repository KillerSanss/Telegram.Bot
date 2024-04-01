using FluentValidation;

namespace Domain.Validations.Validators;

/// <summary>
/// Валидация Person
/// </summary>
public class FullNameValidator : AbstractValidator<string>
{
    public FullNameValidator(string paramName)
    {
        RuleFor(f => f)
            .NotNull().WithMessage(string.Format(ErrorMessages.NullError, paramName))
            .NotEmpty().WithMessage(string.Format(ErrorMessages.EmptyError, paramName))
            .Matches(RegexPatterns.LettersPattern).WithMessage(string.Format(ErrorMessages.OnlyLetters, paramName));
    }
    
}