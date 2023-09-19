using System.Net.Http.Headers;

namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal abstract class ParametrizableBymaHttpConfigBase : IParametrizableBymaHttpConfig
    {
        private readonly string _url;
        private readonly IRequestFactory _requestFactory;
        private readonly IHandlerFactory _handlerFactory;

        protected ParametrizableBymaHttpConfigBase(string url, IRequestFactory requestFactory, IHandlerFactory handlerFactory)
        {
            _url = url;
            _requestFactory = requestFactory;
            _handlerFactory = handlerFactory;
        }

        public HttpClientHandler CreateHandler()
            => _handlerFactory.CreateHandler();

        public HttpRequestMessage CreateRequest(string parameters)
            => _requestFactory.CreateRequest(_url, parameters);
    }
}
