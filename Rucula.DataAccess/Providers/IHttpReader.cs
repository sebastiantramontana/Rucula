namespace Rucula.DataAccess.Providers
{
    internal interface IHttpReader
    {
        Task<string> Read(HttpRequestMessage request);
    }
}
