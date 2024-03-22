using System.Runtime.CompilerServices;
using Ardalis.GuardClauses;
using Domain.Validations.Exceptions;
using Domain.Validations.Primitives;

namespace Domain.Validations.GuardClasses;

/// <summary>
/// Гуард для проверки даты рождения
/// </summary>
public static class BirthDateGuard
{
    /// <summary>
    /// Метод для проверки date на соответствие
    /// </summary>
    public static DateTime BirthDate(
        this IGuardClause guardClause,
        DateTime birthDate)
    {
        Guard.Against.Default(birthDate);
        Guard.Against.FutureDate(birthDate, string.Format(ErrorMessages.FutureDate, nameof(birthDate)));

        if (DateTime.Now.Year - birthDate.Year > 150)
        {
            throw new GuardValidationException(string.Format(ErrorMessages.OldDate, nameof(birthDate)));
        }

        return birthDate;
    }
}