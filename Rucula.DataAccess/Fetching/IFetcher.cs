namespace Rucula.DataAccess.Fetching
{
    internal interface IFetcher
    {
        Task<string> Fetch();
    }
}
