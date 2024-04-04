using Domain.Entities;

namespace Application.Interfaces.Repositories;

/// <summary>
/// Базовый репозиторий
/// </summary>
/// <typeparam name="TEntity">Сущность.</typeparam>
public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    /// <summary>
    /// Получение по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Сущность.</returns>
    public Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение всех
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Список сущностей.</returns>
    public Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Создание
    /// </summary>
    /// <param name="entity">Сущность на добавление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Созданная сущнсоть.</returns>
    public Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление
    /// </summary>
    /// <param name="entity">Сущность на обновление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленная сущсноть.</returns>
    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// Удаление
    /// </summary>
    /// <param name="entity">Сущность на удаление.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Результат удаления.</returns>
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);
}