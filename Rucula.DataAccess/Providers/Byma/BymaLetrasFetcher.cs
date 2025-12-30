using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma;

internal sealed class BymaLetrasFetcher(ILetrasRequestFactory letrasRequestFactory, IHttpReader httpReader) : BymaFetcherBase("BymaLetras", letrasRequestFactory, httpReader), IBymaLetrasFetcher
{
}
