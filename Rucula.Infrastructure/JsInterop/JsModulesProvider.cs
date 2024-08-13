using Microsoft.JSInterop;

namespace Rucula.Infrastructure.JsInterop;

internal class JsModulesProvider : IJsModulesProvider
{
    private readonly IJsModuleImporter _jsModuleImporter;
    private IJSInProcessObjectReference? _jsMainModule;

    public JsModulesProvider(IJsModuleImporter jsModuleImporter)
        => _jsModuleImporter = jsModuleImporter;

    public async ValueTask<IJSInProcessObjectReference> GetMainModule()
        => _jsMainModule ??= await _jsModuleImporter.Import("./main.js").ConfigureAwait(false);
}
