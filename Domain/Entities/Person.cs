using Domain.Entities.ValueObjects;
using Domain.Validations.Primitives;
using Domain.Validations.Validators;
using FluentValidation;

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
    /// Конструктор
    /// </summary>
    /// <param name="fullName">Имя.</param>
    /// <param name="gender">Гендер.</param>
    /// <param name="birthDate">Дата рождения.</param>
    /// <param name="phoneNumber">Номер телефона.</param>
    /// <param name="telegram">Никнейм в телеграм.</param>
    public Person(FullName fullName, Gender gender, DateTime birthDate, string phoneNumber, string telegram)
    {
        FullName = fullName;
        Gender = ValidateGender(gender, nameof(gender), [Gender.None]);
        BirthDate = BirthDateValidation(birthDate, nameof(birthDate));
        PhoneNumber = PhoneValidation(phoneNumber, nameof(phoneNumber));
        Telegram = TelegramValidation(telegram, nameof(telegram));
    }
    
    private string PhoneValidation(string phoneNumber, string paramName)
    {
        var phoneValidator = new PhoneValidator(paramName);
        var phoneValidationResult = phoneValidator.Validate(phoneNumber);
        if (!phoneValidationResult.IsValid)
        {
            throw new ValidationException(phoneValidationResult.Errors);
        }
            
        return phoneNumber;
    }

    private string TelegramValidation(string telegram, string paramName)
    {
        var telegramValidator = new TelegramValidator(paramName);
        var telegramValidationResult = telegramValidator.Validate(telegram);
        if (!telegramValidationResult.IsValid)
        {
            throw new ValidationException(telegramValidationResult.Errors);
        }
            
        return telegram;
    }

    private DateTime BirthDateValidation(DateTime birthDate, string paramName)
    {
        var birthDateValidator = new BirthDateValidator(paramName);
        var birthDateValidationResult = birthDateValidator.Validate(birthDate);
        if (!birthDateValidationResult.IsValid)
        {
            throw new ValidationException(birthDateValidationResult.Errors);
        }
            
        return birthDate;
    }

    private Gender ValidateGender(Gender gender, string paramName, Gender[] defaultValues)
    {
        var enumValidator = new EnumValidator<Gender>(paramName, defaultValues);
        var enumValidationResult = enumValidator.Validate(gender);
        if (!enumValidationResult.IsValid)
        {
            throw new ValidationException(enumValidationResult.Errors);
        }
            
        return gender;
    }
    
    // Проверить на допустимы только буквы русского или англ алф.
    // Телеграм начианется с @
    // Телефон +373 далее 3 цифры от 4 до 9 и далее 5 цифр - regex
}