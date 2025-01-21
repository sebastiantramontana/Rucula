using Microsoft.JSInterop;
using Rucula.Domain.Abstractions;
using Rucula.Infrastructure.JsInterop;

namespace Rucula.Infrastructure;

internal class JsNotifier(IJsModulesProvider jsModulesProvider) : INotifier
{
    public async Task Notify(string message)
    {
        var module = await jsModulesProvider.GetMainModule().ConfigureAwait(false);
        module.InvokeVoid("notify", message);
    }
}
