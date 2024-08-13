using Microsoft.JSInterop;
using Rucula.Domain.Abstractions;
using Rucula.Infrastructure.JsInterop;

namespace Rucula.Infrastructure;

internal class JsNotifier : INotifier
{
    private readonly IJsModulesProvider _jsModulesProvider;

    public JsNotifier(IJsModulesProvider jsModulesProvider)
        => _jsModulesProvider = jsModulesProvider;

    public async Task NotifyProgress(string message)
    {
        var module = await _jsModulesProvider.GetMainModule().ConfigureAwait(false);
        module.InvokeVoid("notify", message);
    }
}
