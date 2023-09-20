namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal interface IRequestFactory
    {
        HttpRequestMessage CreateRequest(string url, string parameters);
    }
}
