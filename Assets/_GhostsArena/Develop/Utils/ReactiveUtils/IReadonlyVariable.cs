using System;

public interface IReadonlyVariable <T>
{
    event Action<T, T> Changed;

    T Value { get; }
}
