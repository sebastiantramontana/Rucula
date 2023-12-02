using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Elements;

internal class ToTableModel<TViewModel> : IToTableModel<TViewModel>
{
    private readonly CollectionTableModel _collectionTableModel;

    internal ToTableModel(CollectionTableModel collectionTableModel)
    {
        _collectionTableModel = collectionTableModel;
    }

    public IFinalizableTableRowModel<TViewModel> RowsFrom(RowSelector rowSelection)
    {
        _collectionTableModel.RowSelector = rowSelection;

        var finalizableTableRowModel = new FinalizableTableRowModel<TViewModel>();
        _collectionTableModel.ModelBuilderData = finalizableTableRowModel;

        return finalizableTableRowModel;
    }
}

