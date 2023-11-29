using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling.Models
{
    internal record class CollectionTableModel(Delegate CollectionFunc)
    {
        internal ElementSelector TableSelector { get; set; } = default!;
        internal RowSelector RowSelection { get; set; } = default!;
        internal IModelBuilderData ModelBuilderData { get; set; } = default!;
    }
}
