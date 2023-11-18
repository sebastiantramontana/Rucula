using WasmViewUpdater.Modeling;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.JsGeneration
{
    internal interface IJsGenerator<TEntity>
    {
        IJsExecutor<TEntity> GenerateJsCode(IEnumerable<ValueModel> values, IEnumerable<CollectionTableModel> collectionTables);
    }
}
