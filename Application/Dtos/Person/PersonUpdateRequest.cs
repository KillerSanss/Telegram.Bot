namespace Application.Dtos.Person;

/// <summary>
/// Дто для обновления Person
/// </summary>
public class PersonUpdateRequest : BasePersonDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }
}