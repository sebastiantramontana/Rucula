namespace Rucula.DataAccess.Providers;

internal interface IHttpReader
{
    Task<string> Read(string readerKey, HttpRequestMessage request);
}
