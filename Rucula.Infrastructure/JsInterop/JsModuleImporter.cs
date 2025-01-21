using Microsoft.JSInterop;

namespace Rucula.Infrastructure.JsInterop;

internal class JsModuleImporter(IJSRuntime jsInProcessRuntime) : IJsModuleImporter
{
    private readonly JSInProcessRuntime _jsInProcessRuntime = (JSInProcessRuntime)jsInProcessRuntime;

    public ValueTask<IJSInProcessObjectReference> Import(string modulePath)
        => _jsInProcessRuntime.InvokeAsync<IJSInProcessObjectReference>("import", modulePath);
}
