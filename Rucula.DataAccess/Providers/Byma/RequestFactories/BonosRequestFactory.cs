
namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal class BonosRequestFactory : IBonosRequestFactory
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/public-bonds";
        private const string JsonContent = @"{ ""T1"": false, ""T0"": true }";
        private readonly IBymaRequestFactory _bymaRequestFactory;

        public BonosRequestFactory(IBymaRequestFactory bymaRequestFactory)
        {
            _bymaRequestFactory = bymaRequestFactory;
        }

        public HttpRequestMessage CreateRequest() => _bymaRequestFactory.CreateRequestPost(Url, JsonContent);
    }
}
