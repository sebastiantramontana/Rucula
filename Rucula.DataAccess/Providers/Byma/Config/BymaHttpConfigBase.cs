namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal abstract class BymaHttpConfigBase : IBymaHttpConfig
    {
        private readonly string _url;
        private readonly string _jsonContent;
        private readonly IRequestFactory _requestFactory;
        private readonly IHandlerFactory _handlerFactory;

        protected BymaHttpConfigBase(string url, string jsonContent, IRequestFactory requestFactory, IHandlerFactory handlerFactory)
        {
            _url = url;
            _jsonContent = jsonContent;
            _requestFactory = requestFactory;
            _handlerFactory = handlerFactory;
        }

        public HttpRequestMessage CreateRequest() => _requestFactory.CreateRequest(_url, _jsonContent);
        public HttpClientHandler CreateHandler() => _handlerFactory.CreateHandler();
    }
}
