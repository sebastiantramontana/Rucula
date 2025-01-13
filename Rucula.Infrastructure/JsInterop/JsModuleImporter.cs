using Microsoft.JSInterop;

namespace Rucula.Infrastructure.JsInterop;

internal class JsModuleImporter : IJsModuleImporter
{
    private readonly JSInProcessRuntime _jsInProcessRuntime;

    public JsModuleImporter(IJSRuntime jsInProcessRuntime)
        => _jsInProcessRuntime = (JSInProcessRuntime)jsInProcessRuntime;

    public IJSInProcessObjectReference Import(string modulePath)
        => _jsInProcessRuntime.Invoke<IJSInProcessObjectReference>("import", modulePath);
}
