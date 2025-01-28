using Microsoft.JSInterop;

namespace Rucula.Infrastructure.JsInterop;

internal sealed class JsModulesProvider(IJsModuleImporter jsModuleImporter) : IJsModulesProvider
{
    private IJSInProcessObjectReference? _jsNotifyModule;

    public async ValueTask<IJSInProcessObjectReference> GetNotifyModule()
        => _jsNotifyModule ??= await jsModuleImporter.Import("./modules/notify.js").ConfigureAwait(false);
}
