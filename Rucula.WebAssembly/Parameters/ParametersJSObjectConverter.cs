using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;
using System.Runtime.InteropServices.JavaScript;

namespace Rucula.WebAssembly.Parameters;

internal sealed class ParametersJSObjectConverter(
    IJSObjectConverter<BondCommissions> bondJSObjectConverter,
    IJSObjectConverter<WesternUnionParameters> wuJSObjectConverter,
    IJSObjectConverter<DolarCryptoParameters> cryptojSObjectConverter) : IParametersJSObjectConverter
{
    public OptionalOptionParameters GetParameters(JSObject? bondCommissionsJSObject, JSObject? westernUnionParametersJSObject, JSObject? dolarCryptoParametersJSObject)
    {
        var bondCommissions = Resolve(bondCommissionsJSObject, bondJSObjectConverter);
        var dolarCryptoParameters = Resolve(dolarCryptoParametersJSObject, cryptojSObjectConverter);
        var westernUnionParameters = Resolve(westernUnionParametersJSObject, wuJSObjectConverter);

        return new(bondCommissions, dolarCryptoParameters, westernUnionParameters);
    }

    private static Optional<T> Resolve<T>(JSObject? jSObject, IJSObjectConverter<T> converter)
        => jSObject is null ? Optional<T>.Empty : Optional<T>.Sure(converter.Convert(jSObject));
}