namespace Rucula.DataAccess.Providers.Byma.RequestFactories;

internal sealed class LetrasRequestFactory(IBymaRequestPostFactory bymaRequestFactory) : ILetrasRequestFactory
{
    private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/lebacs";
    private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T1"": false, ""T0"": true }";

    public HttpRequestMessage CreateRequest() => bymaRequestFactory.CreateRequestPost(Url, JsonContent);
}
