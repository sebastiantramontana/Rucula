using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Elements;

internal class ToTableModel<TViewModel> : IToTableModel<TViewModel>
{
    private readonly CollectionTableModel _collectionTableModel;

    public ToTableModel(CollectionTableModel collectionTableModel)
    {
        _collectionTableModel = collectionTableModel;
    }

    public IFinalizableTableRowModel<TViewModel> FillRows(IRowSelection rowSelection)
    {
        _collectionTableModel.RowSelection = rowSelection;
        return new FinalizableTableRowModel<TViewModel>();
    }
}

