using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling.Building.Elements
{
    public interface IToTableModel<TViewModel>
    {
        IFinalizableTableRowModel<TViewModel> FillRows(RowSelector rowSelection);
    }
}
