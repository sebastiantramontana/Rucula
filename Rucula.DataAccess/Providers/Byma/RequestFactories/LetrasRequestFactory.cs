
namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal class LetrasRequestFactory : ILetrasRequestFactory
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/lebacs";
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T2"": false, ""T1"": false, ""T0"": true }";
        private readonly IBymaRequestPostFactory _bymaRequestFactory;

        public LetrasRequestFactory(IBymaRequestPostFactory bymaRequestFactory)
        {
            _bymaRequestFactory = bymaRequestFactory;
        }

        public HttpRequestMessage CreateRequest() => _bymaRequestFactory.CreateRequestPost(Url, JsonContent);
    }
}
