namespace Rucula.DataAccess.Helpers;

public abstract class InvocationChainBase
{
    public InvocationChainBase(InvocationChainBase? previous)
        => Previous = previous;

    public abstract object? GetValue();
    public InvocationChainBase? Previous { get; }
}
