using Ardalis.GuardClauses;
using Domain.Entities.ValueObjects;
using Domain.Primitives.Enums;
using Domain.Validations.Validators;
using Domain.Validations;

namespace Domain.Entities;

/// <summary>
/// Сущность человека
/// </summary>
public class Person : BaseEntity
{
    /// <summary>
    /// Имя
    /// </summary>
    public FullName FullName { get; set; }
    
    /// <summary>
    /// Гендер
    /// </summary>
    public Gender Gender { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Возраст
    /// </summary>
    public int Age => DateTime.Now.Year - BirthDate.Year;

    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// Никнейм в телеграм
    /// </summary>
    public string Telegram { get; set; }
    
    /// <summary>
    /// Кастомные поля
    /// </summary>
    public List<CustomField<string>> CustomFields { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="fullName">Имя.</param>
    /// <param name="gender">Гендер.</param>
    /// <param name="birthDate">Дата рождения.</param>
    /// <param name="phoneNumber">Номер телефона.</param>
    /// <param name="telegram">Никнейм в телеграм.</param>
    public Person(Guid id, FullName fullName, Gender gender, DateTime birthDate, string phoneNumber, string telegram)
    {
        SetId(id);
        FullName = Guard.Against.Null(fullName);
        Gender = new EnumValidator<Gender>(nameof(gender), [Gender.None]).ValidateWithErrors(gender);
        BirthDate = new BirthDateValidator(nameof(birthDate)).ValidateWithErrors(birthDate);
        PhoneNumber = new PhoneValidator(nameof(phoneNumber)).ValidateWithErrors(phoneNumber);
        Telegram = new TelegramValidator(nameof(telegram)).ValidateWithErrors(telegram);
    }
    
    // Проверить на допустимы только буквы русского или англ алф.
    // Телеграм начианется с @
    // Телефон +373 далее 3 цифры от 4 до 9 и далее 5 цифр - regex
}