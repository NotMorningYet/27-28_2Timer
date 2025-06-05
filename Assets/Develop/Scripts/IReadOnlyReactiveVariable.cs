using System;

public interface IReadOnlyReactiveVariable<T> where T : IEquatable<T>
{
    event Action<T> Changed;
    T Value { get; }
}
