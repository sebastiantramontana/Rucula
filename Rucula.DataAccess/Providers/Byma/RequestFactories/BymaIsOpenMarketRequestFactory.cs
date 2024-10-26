
namespace Rucula.DataAccess.Providers.Byma.RequestFactories;

internal class BymaIsOpenMarketRequestFactory : IBymaIsOpenMarketRequestFactory
{
    private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/market-open";

    public HttpRequestMessage CreateRequest() => new(HttpMethod.Get, Url);
}
