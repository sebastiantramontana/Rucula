using Microsoft.JSInterop;

namespace Rucula.Infrastructure.JsInterop;

internal interface IJsModuleImporter
{
    ValueTask<IJSInProcessObjectReference> Import(string modulePath);
}