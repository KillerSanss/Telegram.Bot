using Domain.Entities;

namespace Application.Interfaces.Repositories;

/// <summary>
/// Репозиторий Person
/// </summary>
public interface IPersonRepository : IBaseRepository<Person>
{
    /// <summary>
    /// Получение кастомных полей
    /// </summary>
    /// <returns>Список кастомных полей.</returns>
    public List<CustomField<string>> GetCustomFields();
}