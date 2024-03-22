using System.ComponentModel.DataAnnotations;
using Domain.Validations.Validators;

namespace Domain.Entities.ValueObjects;

/// <summary>
/// Value Object для полного имени
/// </summary>
public class FullName : BaseValueObject
{
    /// <summary>
    /// Имя
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Отчество
    /// </summary>
    public string? MiddleName { get; set; } = null;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="firstName">Имя.</param>
    /// <param name="lastName">Фамилия.</param>
    /// <param name="middleName">Отчество.</param>
    public FullName(string firstName, string lastName, string? middleName)
    {
        FirstName = ValidateName(firstName, nameof(firstName));
        LastName = ValidateName(lastName, nameof(lastName));
        if (middleName is not null)
        {
            MiddleName = ValidateName(middleName, nameof(middleName));
        }
    }
    
    private static string ValidateName(string value, string paramName)
    {
        var fullNameValidator = new FullNameValidator(paramName);
        var fullNameValidationResult = fullNameValidator.Validate(value);
        if (!fullNameValidationResult.IsValid)
        {
            throw new ValidationException(fullNameValidationResult.Errors.ToString());
        }
        return value;
        
    }
}