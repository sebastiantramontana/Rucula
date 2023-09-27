namespace Rucula.DataAccess.Providers
{
    internal interface IRequestFactory
    {
        HttpRequestMessage CreateRequest(string url, string parameters);
    }
}
