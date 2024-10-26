using System.Net.Http.Headers;

namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal abstract class ParametrizableRequestFactoryBase : IParametrizableRequestFactory
    {
        private readonly string _url;
        private readonly IBymaRequestPostFactory _bymaRequestFactory;

        protected ParametrizableRequestFactoryBase(string url, IBymaRequestPostFactory bymaRequestFactory)
        {
            _url = url;
            _bymaRequestFactory = bymaRequestFactory;
        }

        public HttpRequestMessage CreateRequest(string parameters)
            => _bymaRequestFactory.CreateRequestPost(_url, parameters);
    }
}
