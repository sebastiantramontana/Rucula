using System.Net.Http.Headers;

namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal abstract class FixedRequestFactoryBase : IFixedRequestFactory
    {
        private readonly string _url;
        private readonly string _jsonContent;
        private readonly IBymaRequestFactory _bymaRequestFactory;

        protected FixedRequestFactoryBase(string url, string jsonContent, IBymaRequestFactory bymaRequestFactory)
        {
            _url = url;
            _jsonContent = jsonContent;
            _bymaRequestFactory = bymaRequestFactory;
        }

        public HttpRequestMessage CreateRequest()
            => _bymaRequestFactory.CreateRequest(_url, _jsonContent);
    }
}
