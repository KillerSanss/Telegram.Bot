namespace Domain.Entities;

/// <summary>
/// Базовый класс для всех сущностей
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Переопределие метода Equals
    /// </summary>
    /// <param name="obj"></param>
    public override bool Equals(object? obj)
    {
        if (obj == null)
            return false;

        if (obj is not BaseEntity entity)
            return false;

        if (Id != entity.Id)
            return false;

        if (this.GetHashCode() != entity.GetHashCode())
        {
            return false;
        }
        
        return true;
    }

    /// <summary>
    /// Переопределение метода GetHashCode
    /// </summary>
    /// <returns>Хэш-код.</returns>
    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}