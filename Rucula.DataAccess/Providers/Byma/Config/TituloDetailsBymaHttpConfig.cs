namespace Rucula.DataAccess.Providers.Byma.Config
{
    internal class TituloDetailsBymaHttpConfig : ParametrizableBymaHttpConfigBase, ITituloDetailsBymaHttpConfig
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/bnown/fichatecnica/especies/general";

        public TituloDetailsBymaHttpConfig(IRequestFactory requestFactory)
            : base(Url, requestFactory)
        {
        }
    }
}
