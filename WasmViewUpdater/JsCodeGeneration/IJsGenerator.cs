using WasmViewUpdater.Modeling;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.JsGeneration
{
    internal interface IJsGenerator<TViewModel>
    {
        IJsExecutor<TViewModel> GenerateJsCode(IEnumerable<ValueModel> values, IEnumerable<CollectionTableModel> collectionTables);
    }
}
