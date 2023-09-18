using Rucula.DataAccess.Fetching.Byma.Config;

namespace Rucula.DataAccess.Fetching.Byma
{
    internal interface IBymaHttpReader
    {
        Task<string> Read(HttpRequestMessage request, HttpClientHandler handler);
    }
}
