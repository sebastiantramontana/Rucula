using System.Net.Http.Headers;

namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal abstract class BymaHttpConfigBase : IBymaHttpConfig
    {
        protected BymaHttpConfigBase(string url, string jsonContent, IRequestFactory requestFactory, IHandlerFactory handlerFactory)
        {
            Request = requestFactory.CreateRequest(url, jsonContent);
            Handler = handlerFactory.CreateHandler();
        }

        public HttpRequestMessage Request { get; }
        public HttpClientHandler Handler { get; }
    }
}
