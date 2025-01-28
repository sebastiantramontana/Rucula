namespace Rucula.DataAccess.Providers.Byma.RequestFactories;

internal sealed class TituloDetailsRequestFactory(IBymaRequestPostFactory bymaRequestFactory) : ParametrizableRequestFactoryBase(Url, bymaRequestFactory), ITituloDetailsRequestFactory
{
    private const string Url = "https://open.bymadata.com.ar/vanoms-be-core/rest/api/bymadata/free/bnown/fichatecnica/especies/general";
}
