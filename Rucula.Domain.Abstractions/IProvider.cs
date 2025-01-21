namespace Rucula.Domain.Abstractions;

public interface IProvider<T>
{
    Task<IEnumerable<T>> Get();
}