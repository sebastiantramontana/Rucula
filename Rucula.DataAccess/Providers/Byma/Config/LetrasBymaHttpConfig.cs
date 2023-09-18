namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal class LetrasBymaHttpConfig : BymaHttpConfigBase, ILetrasBymaHttpConfig
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/lebacs";
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T2"": true, ""T1"": true, ""T0"": true }";

        public LetrasBymaHttpConfig()
            : base(Url, JsonContent)
        {
        }
    }
}
