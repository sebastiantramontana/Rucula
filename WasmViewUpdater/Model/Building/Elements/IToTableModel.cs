using WasmViewUpdater.Model.Building.Finalizables;
using WasmViewUpdater.Model.Building.Selectors.TableRows;

namespace WasmViewUpdater.Model.Building.Elements
{
    public interface IToTableModel<TEntity>
    {
        IFinalizableTableRowModel<TEntity> FillRows(IRowSelection rowSelection);
    }
}
