using System.Net.Http.Headers;

namespace Rucula.DataAccess.Providers.Byma.RequestFactories;

internal class BymaRequestPostFactory : IBymaRequestPostFactory
{
    public HttpRequestMessage CreateRequestPost(string url, string parameters)
        => new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(parameters, new MediaTypeHeaderValue("application/json"))
        };
}
