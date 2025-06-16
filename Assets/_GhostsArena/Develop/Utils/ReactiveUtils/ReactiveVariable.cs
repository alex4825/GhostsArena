using System;

public class ReactiveVariable<T>: IReadonlyVariable<T> where T : IEquatable<T>
{
    private T _value;

    public ReactiveVariable(T value) => Value = value;

    public ReactiveVariable() => Value = default(T);

    public event Action<T, T> Changed;

    public T Value
    {
        get => _value;

        set
        {
            T oldValue = _value;

            _value = value;

            if (_value.Equals(oldValue) == false)
                Changed?.Invoke(oldValue, _value);
        }
    }
}
