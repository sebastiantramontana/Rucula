namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal interface IParametrizableBymaHttpConfig
    {
        HttpRequestMessage CreateRequest(string parameters);
        HttpClientHandler CreateHandler();
    }
}
