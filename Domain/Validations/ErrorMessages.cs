namespace Domain.Validations;

/// <summary>
/// Класс сообщений об ошибках
/// </summary>
public abstract class ErrorMessages
{
    /// <summary>
    /// Сообщение об ошибке некорректного формата номера телефона
    /// </summary>
    public const string PhoneFormat = "Phone must be correct format.";

    /// <summary>
    /// Сообщение об ошибке некорректного формата никнейма телеграма
    /// </summary>
    public const string TelegramFormat = "Telegram nickname must be correct format. Example: @example_1";

    /// <summary>
    /// Сообщение об ошибке формата - только буквы
    /// </summary>
    /// <remarks>
    /// Использовать вместе со string.Format
    /// {0} - имя свойства
    /// </remarks>
    public const string OnlyLetters = "{0} must contains only letters";
    
    /// <summary>
    /// Сообщение об ошибке даты
    /// </summary>
    /// <remarks>
    /// Использовать вместе со string.Format
    /// {0} - имя свойства
    /// </remarks>
    public const string FutureDate = "{0} cannot be in future";
    
    /// <summary>
    /// Сообщение об ошибке даты рождения
    /// </summary>
    /// <remarks>
    /// Использовать вместе со string.Format
    /// {0} - имя свойства
    /// </remarks>
    public const string OldDate = "{0} is to much in past";

    /// <summary>
    /// Сообщение об исключении null
    /// </summary>
    /// <remarks>
    /// Использовать вместе со string.Format
    /// {0} - имя свойства
    /// </remarks>
    public const string NullError = "{0} is null";

    /// <summary>
    /// Сообщение об исключении empty
    /// </summary>
    /// <remarks>
    /// Использовать вместе со string.Format
    /// {0} - имя свойства
    /// </remarks>
    public const string EmptyError = "{0} is empty";
    
    /// <summary>
    /// Сообщение об ошибке перечисления
    /// </summary>
    /// <remarks>
    /// Использовать вместе со string.Format
    /// {0} - имя Перечисления
    /// </remarks>
    public const string DefaultEnum = "Enum {0} cannot be default";

}