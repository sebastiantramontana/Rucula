namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal class LetrasBymaHttpConfig : BymaHttpConfigBase, ILetrasBymaHttpConfig
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/lebacs";
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T2"": false, ""T1"": false, ""T0"": true }";

        public LetrasBymaHttpConfig(IRequestFactory requestFactory)
            : base(Url, JsonContent, requestFactory)
        {
        }
    }
}
