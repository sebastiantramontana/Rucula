using Microsoft.JSInterop;

namespace Rucula.Infrastructure.JsInterop;

internal interface IJsModuleImporter
{
    IJSInProcessObjectReference Import(string modulePath);
}