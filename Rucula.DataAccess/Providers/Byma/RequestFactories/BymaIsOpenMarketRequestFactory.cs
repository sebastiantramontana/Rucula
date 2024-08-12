
namespace Rucula.DataAccess.Providers.Byma.RequestFactories;

internal class BymaIsOpenMarketRequestFactory : IBymaIsOpenMarketRequestFactory
{
    private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/market-open";
    private readonly IBymaRequestFactory _bymaRequestFactory;

    public BymaIsOpenMarketRequestFactory(IBymaRequestFactory bymaRequestFactory)
    {
        _bymaRequestFactory = bymaRequestFactory;
    }

    public HttpRequestMessage CreateRequest() => _bymaRequestFactory.CreateRequestGet(Url);
}
