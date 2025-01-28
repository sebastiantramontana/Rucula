using Rucula.Domain.Entities;
using System.Runtime.InteropServices.JavaScript;
using System.Runtime.Versioning;

namespace Rucula.WebAssembly.Parameters;

[SupportedOSPlatform("browser")]
internal sealed class BondCommissionsJSObjectConverter : IJSObjectConverter<BondCommissions>
{
    public BondCommissions Convert(JSObject jsObj)
        => new(jsObj.GetPropertyAsDouble("purchasePercentage"),
               jsObj.GetPropertyAsDouble("salePercentage"),
               jsObj.GetPropertyAsDouble("withdrawalPercentage"));
}
