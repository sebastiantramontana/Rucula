using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma;

internal abstract class BymaFetcherBase(string readerKey, IFixedRequestFactory fixedRequestFactory, IHttpReader httpReader) : IFetcher
{
    public async Task<string> Fetch()
    {
        using var request = fixedRequestFactory.CreateRequest();
        return await httpReader.Read(readerKey, request);
    }
}
