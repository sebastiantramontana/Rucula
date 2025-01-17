using System.Runtime.InteropServices.JavaScript;

namespace Rucula.WebAssembly.Parameters;

internal interface IParametersJSObjectConverter
{
    AllParameters GetParameters(JSObject bondCommissionsJSObject, JSObject westernUnionParametersJSObject, JSObject dolarCryptoParametersJSObject);
}
