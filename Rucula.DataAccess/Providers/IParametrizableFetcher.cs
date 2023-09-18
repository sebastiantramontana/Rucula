namespace Rucula.DataAccess.Fetching
{
    internal interface IParametrizableFetcher
    {
        Task<string> Fetch(string parameters);
    }
}
