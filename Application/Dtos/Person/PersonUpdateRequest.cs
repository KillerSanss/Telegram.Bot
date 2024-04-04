namespace Application.Dtos.Person;

/// <summary>
/// Дто для обновления Person
/// </summary>
public abstract class PersonUpdateRequest : BasePersonDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }
}