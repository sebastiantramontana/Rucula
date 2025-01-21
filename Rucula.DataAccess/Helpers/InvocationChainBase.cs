namespace Rucula.DataAccess.Helpers;

public abstract class InvocationChainBase(InvocationChainBase? previous)
{
    public abstract object? GetValue();
    public InvocationChainBase? Previous { get; } = previous;
}
