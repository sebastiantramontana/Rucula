
namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal class BonosRequestFactory : IBonosRequestFactory
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/public-bonds";
        private readonly IBymaRequestPostFactory _bymaRequestFactory;
        private const string JsonContent = @"{ ""T1"": false, ""T0"": true }";

        public BonosRequestFactory(IBymaRequestPostFactory bymaRequestFactory)
        {
            _bymaRequestFactory = bymaRequestFactory;
        }

        public HttpRequestMessage CreateRequest() => _bymaRequestFactory.CreateRequestPost(Url, JsonContent);
    }
}
