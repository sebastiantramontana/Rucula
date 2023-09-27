namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal interface IParametrizableRequestFactory
    {
        HttpRequestMessage CreateRequest(string parameters);
    }
}
