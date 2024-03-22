using System.Text.Json;

namespace Domain.Entities.ValueObjects;

/// <summary>
/// Базовый класс для Value Object
/// </summary>
public abstract class BaseValueObject
{
    public override bool Equals(object? obj)
    {
        if (obj is not BaseValueObject valueObject)
            return false;

        // var serializedentity = Serialize(entity);
        var serializedValueObject = Serialize(this);

        // прочитать про стринг компар
        // if (serialized.Compare() == 0)
           // return false;

        // изучить дип компар рефлексию
        return true;
    }

    public override int GetHashCode()
    {
        // реализовать
        return 1;
    }

    private string Serialize(BaseValueObject valueObject)
    {
        var serializedValueObject = JsonSerializer.Serialize(valueObject);
        return serializedValueObject;
    }
}