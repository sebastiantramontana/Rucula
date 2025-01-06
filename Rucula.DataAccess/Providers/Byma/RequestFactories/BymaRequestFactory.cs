using System.Net.Http.Headers;

namespace Rucula.DataAccess.Providers.Byma.RequestFactories;

internal class BymaRequestFactory : IBymaRequestFactory
{
    public HttpRequestMessage CreateRequestGet(string url)
        => new(HttpMethod.Get, url);

    public HttpRequestMessage CreateRequestPost(string url, string parameters)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(parameters, new MediaTypeHeaderValue("application/json"))
        };

        request.Headers.CacheControl = new CacheControlHeaderValue() { NoStore = true };

        return request;
    }
}
