using Application.Exceptions.Base;
using Domain.Entities;

namespace Application.Exceptions;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class EntityNotFoundException<T> : BaseNotFoundException where T : BaseEntity
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="paramName">Название параметра.</param>
    /// <param name="value">Значение параметра.</param>
    public EntityNotFoundException(string paramName, string value)
        : base(nameof(T), paramName, value)
    {
    }
}