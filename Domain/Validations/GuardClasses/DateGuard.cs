using Ardalis.GuardClauses;
using Domain.Validations.Exceptions;

namespace Domain.Validations.GuardClasses;

public static class DateGuard
{
    /// <summary>
    /// Метод для проверки даты, не превышающая текущую дату
    /// </summary>
    public static DateTime FutureDate(
        this IGuardClause guardClause,
        DateTime date,
        string message)
    {
        Guard.Against.Default(date);

        if (date > DateTime.Now)
        {
            throw new GuardValidationException(message);
        }

        return date;
    }
}