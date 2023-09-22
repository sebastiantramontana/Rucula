using static System.Net.WebRequestMethods;

namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal class BonosBymaHttpConfig : BymaHttpConfigBase, IBonosBymaHttpConfig
    {
        //private const string CorsProxy = "https://cors-anywhere.herokuapp.com/";
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/public-bonds";
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T2"": false, ""T1"": false, ""T0"": true }";

        public BonosBymaHttpConfig(IRequestFactory requestFactory)
            : base(Url, JsonContent, requestFactory)
        {
        }
    }
}
