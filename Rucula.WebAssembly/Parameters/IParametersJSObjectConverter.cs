using Rucula.Domain.Entities.Parameters;
using System.Runtime.InteropServices.JavaScript;

namespace Rucula.WebAssembly.Parameters;

internal interface IParametersJSObjectConverter
{
    OptionalOptionParameters GetParameters(JSObject? bondCommissionsJSObject, JSObject? westernUnionParametersJSObject, JSObject? dolarCryptoParametersJSObject, JSObject? dolarAppParametersJSObject);
}
