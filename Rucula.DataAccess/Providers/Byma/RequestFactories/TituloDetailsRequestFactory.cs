namespace Rucula.DataAccess.Providers.Byma.RequestFactories
{
    internal class TituloDetailsRequestFactory : ParametrizableRequestFactoryBase, ITituloDetailsRequestFactory
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/bnown/fichatecnica/especies/general";

        public TituloDetailsRequestFactory(IBymaRequestPostFactory bymaRequestFactory)
            : base(Url, bymaRequestFactory)
        {
        }
    }
}
