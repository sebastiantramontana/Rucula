using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Elements;

internal class TableRowsBuilder<TViewModel> : ITableRowsBuilder<TViewModel>
{
    private readonly CollectionTableModel _collectionTableModel;

    internal TableRowsBuilder(CollectionTableModel collectionTableModel)
    {
        _collectionTableModel = collectionTableModel;
    }

    public IFinalizableTableRowBuilder<TViewModel> RowsFrom(RowSelector rowSelection)
    {
        _collectionTableModel.RowSelector = rowSelection;

        var finalizableTableRowModel = new FinalizableTableRowBuilder<TViewModel>();
        _collectionTableModel.ModelBuilderData = finalizableTableRowModel;

        return finalizableTableRowModel;
    }
}

