namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal interface IBymaHttpConfig
    {
        HttpRequestMessage Request { get; }
        HttpClientHandler Handler { get; }
    }
}
