namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal class LetrasRequestFactory : FixedRequestFactoryBase, ILetrasRequestFactory
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/lebacs";
        private const string JsonContent = @"{ ""excludeZeroPxAndQty"": true, ""T2"": false, ""T1"": false, ""T0"": true }";

        public LetrasRequestFactory(IBymaRequestFactory bymaRequestFactory)
            : base(Url, JsonContent, bymaRequestFactory)
        {
        }
    }
}
