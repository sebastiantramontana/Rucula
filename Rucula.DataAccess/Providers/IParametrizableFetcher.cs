namespace Rucula.DataAccess.Providers
{
    internal interface IParametrizableFetcher
    {
        Task<string> Fetch(string parameters);
    }
}
