using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma;

internal sealed class BymaIsOpenMarketFetcher(IBymaIsOpenMarketRequestFactory isOpenMarketRequestFactory, IHttpReader httpReader) : BymaFetcherBase(isOpenMarketRequestFactory, httpReader), IBymaIsMarketOpenFetcher
{
}
