namespace Rucula.DataAccess.Providers
{
    internal interface IFetcher
    {
        Task<string> Fetch();
    }
}
