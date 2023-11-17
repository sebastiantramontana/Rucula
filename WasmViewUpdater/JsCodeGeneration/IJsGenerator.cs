using WasmViewUpdater.Modeling;

namespace WasmViewUpdater.JsGeneration
{
    internal interface IJsGenerator<TEntity>
    {
        IJsExecutor<TEntity> GenerateJsCode(IEnumerable<IModel> models);
    }
}
