using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Helpers;

public sealed class InvocationChainNullable<T> : InvocationChainBase where T : class
{
    public static InvocationChainNullable<T> Create(T? value)
        => new(value, null);

    private readonly T? _value;

    internal InvocationChainNullable(T? value, InvocationChainBase? previous)
        : base(previous)
    {
        _value = value;
    }

    public InvocationChainNullable<TRet> IfNotNull<TRet>(Func<T, TRet?> objFunc) where TRet : class
    {
        var nextValue = _value is not null ? objFunc.Invoke(_value) : null;
        return new(nextValue, this);
    }

    public InvocationChainOptional<TRet> IfNotNull<TRet>(Func<T, Optional<TRet>> objFunc)
    {
        var nextValue = _value is not null ? objFunc.Invoke(_value) : Optional<TRet>.Empty;
        return new(nextValue, this);
    }

    public TRet? Return<TRet>(Func<T, TRet> objFunc) where TRet : class
        => _value is not null ? objFunc.Invoke(_value) : null;

    public Optional<TRet> Return<TRet>(Func<T, Optional<TRet>> objFunc)
        => _value is not null ? objFunc.Invoke(_value) : Optional<TRet>.Empty;

    public override object? GetValue() => _value;
}
