namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal interface IHandlerFactory
    {
        HttpClientHandler CreateHandler();
    }
}
