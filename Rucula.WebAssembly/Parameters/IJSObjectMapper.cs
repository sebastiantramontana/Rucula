using System.Runtime.InteropServices.JavaScript;

namespace Rucula.WebAssembly.Parameters;

internal interface IJSObjectConverter<out T>
{
    T Convert(JSObject jsObj);
}
