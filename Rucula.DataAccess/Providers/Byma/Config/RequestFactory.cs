using System.Net.Http.Headers;

namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal class RequestFactory : IRequestFactory
    {
        public HttpRequestMessage CreateRequest(string url, string parameters)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(parameters);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            request.Headers.Add("Referer-Policy", "no-referrer-when-downgrade");

            return request;
        }
    }
}
