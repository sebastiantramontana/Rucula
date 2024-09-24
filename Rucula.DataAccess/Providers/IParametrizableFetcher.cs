namespace Rucula.DataAccess.Providers;

internal interface IParametrizableFetcher<T>
{
    Task<string> Fetch(T parameters);
}
