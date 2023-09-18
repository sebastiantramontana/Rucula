namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal class BonosBymaHttpConfig : BymaHttpConfigBase, IBonosBymaHttpConfig
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/public-bonds";
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T2"": true, ""T1"": true, ""T0"": true }";

        public BonosBymaHttpConfig(IRequestFactory requestFactory, IHandlerFactory handlerFactory)
            : base(Url, JsonContent, requestFactory, handlerFactory)
        {
        }
    }
}
