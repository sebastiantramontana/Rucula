namespace Rucula.DataAccess.Fetching.Byma.Config
{
    internal class TituloDetailsBymaHttpConfig : ParametrizableBymaHttpConfigBase, ITituloDetailsBymaHttpConfig
    {
        private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/bnown/fichatecnica/especies/general";

        public TituloDetailsBymaHttpConfig(IRequestFactory requestFactory, IHandlerFactory handlerFactory)
            : base(Url, requestFactory, handlerFactory)
        {
        }
    }
}
