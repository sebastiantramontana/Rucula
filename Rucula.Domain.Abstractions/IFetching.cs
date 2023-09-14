namespace Rucula.Domain.Abstractions
{
    public interface IFetching<T>
    {
        Task<IEnumerable<T>> Fetch();
    }
}