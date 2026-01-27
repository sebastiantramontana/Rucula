using Rucula.Domain.Entities.Parameters;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace Rucula.WebAssembly.Parameters;

[SupportedOSPlatform("browser")]
internal sealed class DolarAppParametersJSObjectConverter : IJSObjectConverter<DolarAppParameters>
{
    public DolarAppParameters Convert(JSObject jsObj)
        => new(jsObj.GetPropertyAsDouble("volume"));
}
