namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal class BonosRequestFactory : FixedRequestFactoryBase, IBonosRequestFactory
    {
        //private const string CorsProxy = "https://cors-anywhere.herokuapp.com/";
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/public-bonds";
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T2"": false, ""T1"": false, ""T0"": true }";

        public BonosRequestFactory(IBymaRequestFactory bymaRequestFactory)
            : base(Url, JsonContent, bymaRequestFactory)
        {
        }
    }
}
