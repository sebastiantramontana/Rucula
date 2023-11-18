using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling.Models
{
    internal record class CollectionTableModel(Delegate CollectionFunc, IElementSelector TableSelector, IRowSelection RowSelection, IEnumerable<ValueModel> Values, IEnumerable<CollectionTableModel> CollectionTables);
}
