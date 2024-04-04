using Application.Dtos.Person;

namespace Application.Interfaces.Services;

/// <summary>
/// Интерфейс описывающий PersonService
/// </summary>
public interface IPersonService
{
    /// <summary>
    /// Добавлние нового студента
    /// </summary>
    /// <param name="student">Дто студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Добавленный студент.</returns>
    Task<PersonDtoResponse> CreateAsync(PersonCreateRequest student, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление студента
    /// </summary>
    /// <param name="student">Дто студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Обновленный студент.</returns>
    Task<PersonDtoResponse> UpdateAsync(PersonUpdateRequest student, CancellationToken  cancellationToken);

    /// <summary>
    /// Получение студента
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Выбранный студент.</returns>
    Task<PersonDtoResponse> GetByIdAsync(Guid studentId, CancellationToken cancellationToken);

    /// <summary>
    /// Получение всех студентов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены.</param>
    /// <returns>Массив студентов.</returns>
    Task<List<PersonDtoResponse>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Удаление студента
    /// </summary>
    /// <param name="studentId">Идентификатор студента.</param>
    /// <param name="cancellationToken">Токен отмены.</param>
    Task DeleteAsync(Guid studentId, CancellationToken cancellationToken);
}