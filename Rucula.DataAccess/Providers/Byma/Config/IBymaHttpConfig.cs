namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal interface IBymaHttpConfig
    {
        HttpRequestMessage CreateRequest();
        HttpClientHandler CreateHandler();
    }
}
