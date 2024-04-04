using System.Text.RegularExpressions;

namespace Domain.Validations;

/// <summary>
/// Класс для описания регулярных выражений
/// </summary>
public static class RegexPatterns
{
    /// <summary>
    /// Номер телефона
    /// </summary>
    public static readonly Regex PhonePattern = new(@"^373\d{8}$");

    /// <summary>
    /// Никнейм телеграма
    /// </summary>
    public static readonly Regex TelegramPattern = new("^@[A-Za-z0-9_]$");

    /// <summary>
    /// Строка (только буквы)
    /// </summary>
    public static readonly Regex LettersPattern = new("\\p{L}'?$");
}