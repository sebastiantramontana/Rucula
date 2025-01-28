using Rucula.Domain.Entities;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace Rucula.WebAssembly.Parameters;

[SupportedOSPlatform("browser")]
internal sealed class WesternUnionParametersJSObjectConverter : IJSObjectConverter<WesternUnionParameters>
{
    public WesternUnionParameters Convert(JSObject jsObj)
        => new(jsObj.GetPropertyAsDouble("amountToSend"));
}
