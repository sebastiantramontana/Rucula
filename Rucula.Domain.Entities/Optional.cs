using System.Text.Json.Serialization;

namespace Rucula.Domain.Entities;

public class Optional<T> : IEquatable<Optional<T>>, IEquatable<T>
{
    public static Optional<T> Maybe<T2>(T2? value) where T2 : struct, T
        => (value is null) ? Empty : Optional<T>.Sure(value.Value);

    public static Optional<T> Maybe(T? value)
        => (value is null) ? Empty : Optional<T>.Sure(value);

    public static Optional<T> Sure(T value)
        => new(value, true);

    public static Optional<T> Empty
        => new(default, false);

    [JsonConstructor]
    private Optional(T? value, bool hasValue)
    {
        _value = value;
        HasValue = hasValue;
        IsEmpty = !HasValue;
    }

    public bool HasValue { get; }
    public bool IsEmpty { get; }

    private readonly T? _value = default;
    public T Value => HasValue ? _value! : throw new InvalidOperationException("Optional object has not value");

    public override bool Equals(object? obj)
        => Equals(obj as Optional<T>);

    public bool Equals(Optional<T>? other)
        => other is not null &&
            other.HasValue &&
            Equals(other.Value);

    public bool Equals(T? otherValue)
        => otherValue is not null &&
            HasValue &&
            (Value?.Equals(otherValue) ?? false);

    public override int GetHashCode()
        => !HasValue ? HasValue.GetHashCode() : HashCode.Combine(HasValue, Value);

    public override string? ToString()
        => HasValue ? Value!.ToString() : string.Empty;

}
