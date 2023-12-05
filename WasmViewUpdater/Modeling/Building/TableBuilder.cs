using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class TableBuilder<TViewModel> : ITableBuilder<TViewModel>
{
    private readonly CollectionTableModel _collectionTableModel;

    public TableBuilder(CollectionTableModel collectionTableModel)
    {
        _collectionTableModel = collectionTableModel;
    }

    public ITableRowsBuilder<TViewModel> ToTable(ElementSelector selector)
    {
        _collectionTableModel.TableSelector = selector;
        return new TableRowsBuilder<TViewModel>(_collectionTableModel);
    }
}

