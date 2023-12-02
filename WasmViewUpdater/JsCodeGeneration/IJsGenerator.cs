using WasmViewUpdater.Modeling.Building;

namespace WasmViewUpdater.JsGeneration
{
    internal interface IJsGenerator<TViewModel>
    {
        IJsExecutor<TViewModel> GenerateJsCode(IModelBuilderData modelBuilderData);
    }
}
