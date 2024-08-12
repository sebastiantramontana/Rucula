using System.Net.Http.Headers;

namespace Rucula.DataAccess.Providers.Byma.RequestFactories;

internal class BymaRequestFactory : IBymaRequestFactory
{
    public HttpRequestMessage CreateRequestGet(string url)
        => AddHeaders(new HttpRequestMessage(HttpMethod.Get, url));

    public HttpRequestMessage CreateRequestPost(string url, string parameters)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(parameters, new MediaTypeHeaderValue("application/json"))
        };

        return AddHeaders(request);
    }

    private static HttpRequestMessage AddHeaders(HttpRequestMessage request)
    {
        request.Headers.Add("Referer-Policy", "no-referrer-when-downgrade");
        request.Headers.Add("Access-Control-Allow-Origin", "https://open.bymadata.com.ar");
        request.Headers.Add("Host", "open.bymadata.com.ar");
        request.Headers.Add("Origin", "https://open.bymadata.com.ar");
        request.Headers.Add("Referer", "https://open.bymadata.com.ar/");
        request.Headers.Add("Sec-Ch-Ua-Mobile", "?0");
        request.Headers.Add("Sec-Ch-Ua-Platform", " \"Windows\"");
        request.Headers.Add("Sec-Fetch-Dest", "empty");
        request.Headers.Add("Sec-Fetch-Mode", "cors");
        request.Headers.Add("Sec-Ch-Ua", "\"Google Chrome\";v=\"117\", \"Not;A=Brand\";v=\"8\", \"Chromium\";v=\"117\"");
        request.Headers.Add("Sec-Fetch-Site", "same-origin");
        request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/117.0.0.0 Safari/537.36");

        return request;
    }
}
