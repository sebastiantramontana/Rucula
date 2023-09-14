namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal class BonosBymaHttpConfig : BymaHttpConfigBase, ILetrasBymaHttpConfig
    {
        public const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/public-bonds";
        public const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T2"": true, ""T1"": true, ""T0"": true }";

        public BonosBymaHttpConfig()
            : base(Url, JsonContent)
        {
        }
    }
}
