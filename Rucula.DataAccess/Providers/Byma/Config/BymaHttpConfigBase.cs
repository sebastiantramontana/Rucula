namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal abstract class BymaHttpConfigBase : IBymaHttpConfig
    {
        private readonly string _url;
        private readonly string _jsonContent;
        private readonly IRequestFactory _requestFactory;

        protected BymaHttpConfigBase(string url, string jsonContent, IRequestFactory requestFactory)
        {
            _url = url;
            _jsonContent = jsonContent;
            _requestFactory = requestFactory;
        }

        public HttpRequestMessage CreateRequest() => _requestFactory.CreateRequest(_url, _jsonContent);
    }
}
