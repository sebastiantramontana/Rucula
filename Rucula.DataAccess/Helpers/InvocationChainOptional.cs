﻿using Rucula.Domain.Entities;

namespace Rucula.DataAccess.Helpers;

public sealed class InvocationChainOptional<T> : InvocationChainBase
{
    public static InvocationChainOptional<T> Create(Optional<T> value)
        => new(value, null);

    private readonly Optional<T> _value;

    internal InvocationChainOptional(Optional<T> value, InvocationChainBase? previous)
        : base(previous)
    {
        _value = value;
    }

    public InvocationChainOptional<TRet> IfNotEmpty<TRet>(Func<T, Optional<TRet>> objFunc)
    {
        var nextValue = _value.HasValue ? objFunc.Invoke(_value.Value) : Optional<TRet>.Empty;
        return new(nextValue, this);
    }

    public InvocationChainOptional<TRet> IfNotEmpty<TPrev, TRet>(Func<T, TPrev, Optional<TRet>> objFunc)
    {
        var previousValue = Previous?.GetValue();

        var nextValue = _value.HasValue && previousValue is not null
            ? objFunc.Invoke(_value.Value, (TPrev)previousValue)
            : Optional<TRet>.Empty;

        return new(nextValue, this);
    }

    public InvocationChainNullable<TRet> IfNotEmpty<TRet>(Func<T, TRet?> objFunc) where TRet : class
    {
        var nextValue = _value.HasValue ? objFunc.Invoke(_value.Value) : null;
        return new(nextValue, this);
    }

    public TRet? Return<TRet>(Func<T, TRet?> objFunc) where TRet : class
        => _value.HasValue ? objFunc.Invoke(_value.Value) : null;

    public Optional<TRet> Return<TRet>(Func<T, Optional<TRet>> objFunc)
        => _value.HasValue ? objFunc.Invoke(_value.Value) : Optional<TRet>.Empty;

    public TRet? Return<TPrev, TRet>(Func<T, TPrev, TRet?> objFunc) where TRet : class
    {
        var previousValue = Previous?.GetValue();

        return _value.HasValue && previousValue is not null
            ? objFunc.Invoke(_value.Value, (TPrev)previousValue)
            : null;
    }

    public override object? GetValue() => _value.HasValue ? _value.Value : null;
}
