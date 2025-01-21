using Microsoft.JSInterop;

namespace Rucula.Infrastructure.JsInterop;

internal class JsModulesProvider(IJsModuleImporter jsModuleImporter) : IJsModulesProvider
{
    private IJSInProcessObjectReference? _jsMainModule;

    public async ValueTask<IJSInProcessObjectReference> GetMainModule()
        => _jsMainModule ??= await jsModuleImporter.Import("./main.js").ConfigureAwait(false);
}
