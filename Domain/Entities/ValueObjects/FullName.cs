using System.ComponentModel.DataAnnotations;
using Domain.Validations;
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
        FirstName = new FullNameValidator(nameof(firstName)).ValidateWithErrors(firstName);
        LastName = new FullNameValidator(nameof(lastName)).ValidateWithErrors(lastName);
        if (middleName is not null)
        {
            MiddleName = new FullNameValidator(nameof(middleName)).ValidateWithErrors(middleName);
        }
    }

    /// <summary>
    /// Обновление FullName
    /// </summary>
    /// <param name="firstName">Имя.</param>
    /// <param name="lastName">Фамилия.</param>
    /// <param name="middleName">Отчество.</param>
    /// <returns>Полное имя.</returns>
    public FullName Update(string firstName, string lastName, string middleName)
    {
        if (firstName is not null)
        {
            FirstName = new FullNameValidator(nameof(firstName)).ValidateWithErrors(firstName);
        }

        if (lastName is not null)
        {
            LastName = new FullNameValidator(nameof(lastName)).ValidateWithErrors(lastName);
        }

        if (middleName is not null)
        {
            MiddleName = new FullNameValidator(nameof(middleName)).ValidateWithErrors(middleName);
        }

        return this;
    }

    private FullName()
    {
    }
}