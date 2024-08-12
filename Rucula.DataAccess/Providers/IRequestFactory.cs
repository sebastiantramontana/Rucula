namespace Rucula.DataAccess.Providers;

internal interface IRequestFactory
{
    HttpRequestMessage CreateRequestPost(string url, string parameters);
    HttpRequestMessage CreateRequestGet(string url);
}
