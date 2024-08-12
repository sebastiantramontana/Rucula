using Rucula.DataAccess.Providers.Byma.RequestFactories;

namespace Rucula.DataAccess.Providers.Byma
{
    internal class BymaIsOpenMarketFetcher : BymaFetcherBase, IBymaIsMarketOpenFetcher
    {
        public BymaIsOpenMarketFetcher(IBymaIsOpenMarketRequestFactory isOpenMarketRequestFactory, IHttpReader httpReader)
            : base(isOpenMarketRequestFactory, httpReader) { }
    }
}
