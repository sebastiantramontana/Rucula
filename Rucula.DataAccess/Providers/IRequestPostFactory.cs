namespace Rucula.DataAccess.Providers;

internal interface IRequestPostFactory
{
    HttpRequestMessage CreateRequestPost(string url, string parameters);
}
