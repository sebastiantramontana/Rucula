namespace Rucula.DataAccess.Providers.Byma
{
    internal interface IBymaHttpReader
    {
        Task<string> Read(HttpRequestMessage request);
    }
}
