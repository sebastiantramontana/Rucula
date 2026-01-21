using Rucula.Domain.Entities.Parameters;
using System.Runtime.InteropServices.JavaScript;

namespace Rucula.WebAssembly.Parameters;

internal sealed class ParametersJSObjectConverter(
    IJSObjectConverter<BondCommissions> bondJSObjectConverter,
    IJSObjectConverter<WesternUnionParameters> wuJSObjectConverter,
    IJSObjectConverter<DolarCryptoParameters> cryptojSObjectConverter) : IParametersJSObjectConverter
{
    public OptionParameters GetParameters(JSObject bondCommissionsJSObject, JSObject westernUnionParametersJSObject, JSObject dolarCryptoParametersJSObject)
    {
        var bondCommissions = bondJSObjectConverter.Convert(bondCommissionsJSObject);
        var westernUnionParameters = wuJSObjectConverter.Convert(westernUnionParametersJSObject);
        var dolarCryptoParameters = cryptojSObjectConverter.Convert(dolarCryptoParametersJSObject);
        return new(bondCommissions, dolarCryptoParameters, westernUnionParameters);
    }
}