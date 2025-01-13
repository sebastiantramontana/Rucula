using Microsoft.JSInterop;

namespace Rucula.Infrastructure.JsInterop;

internal class JsModulesProvider : IJsModulesProvider
{
    private readonly IJsModuleImporter _jsModuleImporter;
    private IJSInProcessObjectReference? _jsMainModule;

    public JsModulesProvider(IJsModuleImporter jsModuleImporter)
        => _jsModuleImporter = jsModuleImporter;

    public IJSInProcessObjectReference GetMainModule()
        => _jsMainModule ??= _jsModuleImporter.Import("./main.js");
}
