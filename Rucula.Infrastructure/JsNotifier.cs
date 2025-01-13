using Microsoft.JSInterop;
using Rucula.Domain.Abstractions;
using Rucula.Infrastructure.JsInterop;

namespace Rucula.Infrastructure;

internal class JsNotifier : INotifier
{
    private readonly IJsModulesProvider _jsModulesProvider;

    public JsNotifier(IJsModulesProvider jsModulesProvider)
        => _jsModulesProvider = jsModulesProvider;

    public void NotifyProgress(string message)
    {
        var module = _jsModulesProvider.GetMainModule();
        module.InvokeVoid("notify", message);
    }
}
