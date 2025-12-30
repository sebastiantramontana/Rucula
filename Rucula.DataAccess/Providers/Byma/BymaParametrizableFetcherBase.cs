using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma;

internal abstract class BymaParametrizableFetcherBase(string readerKey, IParametrizableRequestFactory parametrizableRequestFactory, IHttpReader httpReader) : IParametrizableFetcher<string>
{
    public async Task<string> Fetch(string parameters)
    {
        using var request = parametrizableRequestFactory.CreateRequest(parameters);
        return await httpReader.Read(readerKey, request);
    }
}
