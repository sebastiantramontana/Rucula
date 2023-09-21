namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal class HandlerFactory : IHandlerFactory
    {
        public HttpClientHandler CreateHandler()
            => new HttpClientHandler
            {
                //CheckCertificateRevocationList = false,
                //ServerCertificateCustomValidationCallback = (a, b, c, d) => true,
                ClientCertificateOptions = ClientCertificateOption.Manual
            };
    }
}
