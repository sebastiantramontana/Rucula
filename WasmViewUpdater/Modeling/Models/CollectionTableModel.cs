using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling.Models
{
    internal record class CollectionTableModel(Delegate collectionFunc)
    {
        internal Delegate CollectionFunc { get; } = collectionFunc;
        internal IElementSelector TableSelector { get; set; } = default!;
        internal IRowSelection RowSelection { get; set; } = default!;
        internal IEnumerable<ValueModel> Values { get; set; } = default!;
        internal IEnumerable<CollectionTableModel> CollectionTables { get; set; } = default!;
    }
}
