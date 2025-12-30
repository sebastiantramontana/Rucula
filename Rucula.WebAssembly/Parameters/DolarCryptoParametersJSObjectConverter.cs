using Rucula.Domain.Entities.Parameters;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace Rucula.WebAssembly.Parameters;

[SupportedOSPlatform("browser")]
internal sealed class DolarCryptoParametersJSObjectConverter : IJSObjectConverter<DolarCryptoParameters>
{
    public DolarCryptoParameters Convert(JSObject jsObj)
        => new(jsObj.GetPropertyAsDouble("volume"));
}
