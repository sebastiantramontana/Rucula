
namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal class LetrasRequestFactory : ILetrasRequestFactory
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/lebacs";
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T1"": false, ""T0"": true }";
        private readonly IBymaRequestFactory _bymaRequestFactory;

        public LetrasRequestFactory(IBymaRequestFactory bymaRequestFactory)
        {
            _bymaRequestFactory = bymaRequestFactory;
        }

        public HttpRequestMessage CreateRequest() => _bymaRequestFactory.CreateRequestPost(Url, JsonContent);
    }
}
