using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

public interface IModelBuilderData
{
    internal IEnumerable<ValueModel> Values { get; }
    internal IEnumerable<CollectionTableModel> CollectionTables { get; }
}
