namespace Application.Dtos.Person;

/// <summary>
/// Дто ответа на обновление Person
/// </summary>
public class PersonUpdateResponse : BasePersonDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; init; }
}
