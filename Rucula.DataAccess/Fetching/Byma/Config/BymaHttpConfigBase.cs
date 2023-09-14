using System.Net.Http.Headers;

namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal abstract class BymaHttpConfigBase : IBymaHttpConfig
    {
        protected BymaHttpConfigBase(string url, string jsonContent)
        {
            Request = CreateRequest(url, jsonContent);
            Handler = CreateHandler();
        }

        public HttpRequestMessage Request { get; }
        public HttpClientHandler Handler { get; }

        private static HttpRequestMessage CreateRequest(string url, string jsonContent)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(jsonContent);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Add("Referer-Policy", "no-referrer-when-downgrade");

            return request;
        }

        private static HttpClientHandler CreateHandler()
            => new HttpClientHandler
            {
                CheckCertificateRevocationList = false,
                ServerCertificateCustomValidationCallback = (a, b, c, d) => true,
                ClientCertificateOptions = ClientCertificateOption.Manual
            };
    }
}
