using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Dal.EntityFramework;

namespace Infrastructure.Dal.Repositories;

/// <summary>
/// Репозиторий персоны
/// </summary>
public class PersonRepository : IPersonRepository
{
    private readonly TelegramBotDbContext _dbContext;

    public PersonRepository(TelegramBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Получение персоны по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор персоны.</param>
    /// <returns>Выбранная персона.</returns>
    public Person GetById(Guid id)
    {
        return _dbContext.Persons.FirstOrDefault(p => p.Id == id);
    }

    /// <summary>
    /// Получение всех персон из базы данных
    /// </summary>
    /// <returns>Все персоны из базы данных.</returns>
    public List<Person> GetAll()
    {
        return _dbContext.Persons.ToList();
    }

    /// <summary>
    /// Добавление персоны в базу данных
    /// </summary>
    /// <param name="entity">Данные персноны.</param>
    /// <returns>Добавленная персона.</returns>
    public Person Create(Person entity)
    {
        return _dbContext.Persons.Add(entity).Entity;
    }

    /// <summary>
    /// Обновление персоны в базе данных
    /// </summary>
    /// <param name="entity">Данные персоны.</param>
    /// <returns>Обновленная персона.</returns>
    public Person Update(Person entity)
    {
        return _dbContext.Persons.Update(entity).Entity;
    }

    /// <summary>
    /// Удаление персоны
    /// </summary>
    /// <param name="entity">Перснона на удаление.</param>
    public void Delete(Person entity)
    {
        _dbContext.Persons.Remove(entity);
    }
    
    /// <summary>
    /// Получение всех кастомных полей персоны
    /// </summary>
    /// <returns>Список кастомных полей.</returns>
    public List<CustomField<string>> GetCustomFields()
    {
        return _dbContext.CustomFields.ToList();
    }

    /// <summary>
    /// Сохранение данных в базе
    /// </summary>
    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }

    
}