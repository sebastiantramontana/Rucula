using Rucula.Domain.Entities;
using Rucula.Domain.Entities.Parameters;
using System.Runtime.InteropServices.JavaScript;

namespace Rucula.WebAssembly.Parameters;

internal sealed class ParametersJSObjectConverter(
    IJSObjectConverter<BondCommissions> bondJSObjectConverter,
    IJSObjectConverter<WesternUnionParameters> wuJSObjectConverter,
    IJSObjectConverter<DolarCryptoParameters> cryptojSObjectConverter,
    IJSObjectConverter<DolarAppParameters> dolarAppjSObjectConverter) : IParametersJSObjectConverter
{
    public OptionalOptionParameters GetParameters(JSObject? bondCommissionsJSObject, JSObject? westernUnionParametersJSObject, JSObject? dolarCryptoParametersJSObject, JSObject? dolarAppParametersJSObject)
    {
        var bondCommissions = Resolve(bondCommissionsJSObject, bondJSObjectConverter);
        var dolarCryptoParameters = Resolve(dolarCryptoParametersJSObject, cryptojSObjectConverter);
        var westernUnionParameters = Resolve(westernUnionParametersJSObject, wuJSObjectConverter);
        var dolarAppParameters = Resolve(dolarAppParametersJSObject, dolarAppjSObjectConverter);

        return new(bondCommissions, dolarCryptoParameters, westernUnionParameters, dolarAppParameters);
    }

    private static Optional<T> Resolve<T>(JSObject? jSObject, IJSObjectConverter<T> converter)
        => jSObject is null ? Optional<T>.Empty : Optional<T>.Sure(converter.Convert(jSObject));
}