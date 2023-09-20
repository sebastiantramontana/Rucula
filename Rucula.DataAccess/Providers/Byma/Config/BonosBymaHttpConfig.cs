namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal class BonosBymaHttpConfig : BymaHttpConfigBase, IBonosBymaHttpConfig
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/public-bonds";
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T2"": false, ""T1"": false, ""T0"": true }";

        public BonosBymaHttpConfig(IRequestFactory requestFactory, IHandlerFactory handlerFactory)
            : base(Url, JsonContent, requestFactory, handlerFactory)
        {
        }
    }
}
