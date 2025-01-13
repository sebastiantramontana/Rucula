using Microsoft.JSInterop;

namespace Rucula.Infrastructure.JsInterop;

internal interface IJsModulesProvider
{
    IJSInProcessObjectReference GetMainModule();
}