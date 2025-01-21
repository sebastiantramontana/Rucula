
namespace Rucula.DataAccess.Providers.Byma.RequestFactories;

internal class BonosRequestFactory(IBymaRequestPostFactory bymaRequestFactory) : IBonosRequestFactory
{
    private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/public-bonds";
    private const string JsonContent = @"{ ""T1"": false, ""T0"": true }";

    public HttpRequestMessage CreateRequest() => bymaRequestFactory.CreateRequestPost(Url, JsonContent);
}
