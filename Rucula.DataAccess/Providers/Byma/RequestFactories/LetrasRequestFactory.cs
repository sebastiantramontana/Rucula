
namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal class LetrasRequestFactory : ILetrasRequestFactory
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/lebacs";
        private readonly IBymaRequestPostFactory _bymaRequestFactory;
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T1"": false, ""T0"": true }";

        public LetrasRequestFactory(IBymaRequestPostFactory bymaRequestFactory)
        {
            _bymaRequestFactory = bymaRequestFactory;
        }

        public HttpRequestMessage CreateRequest() => _bymaRequestFactory.CreateRequestPost(Url, JsonContent);
    }
}
