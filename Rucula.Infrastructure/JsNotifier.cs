using Microsoft.JSInterop;
using Rucula.Domain.Abstractions;
using Rucula.Infrastructure.JsInterop;

namespace Rucula.Infrastructure;

internal sealed class JsNotifier(IJsModulesProvider jsModulesProvider) : INotifier
{
    public async Task Notify(string message)
    {
        var module = await jsModulesProvider.GetNotifyModule().ConfigureAwait(false);
        module.InvokeVoid("notify", message);
    }
}
