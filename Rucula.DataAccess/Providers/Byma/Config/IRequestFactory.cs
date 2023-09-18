namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal interface IRequestFactory
    {
        HttpRequestMessage CreateRequest(string url, string parameters);
    }
}
