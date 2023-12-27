using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling.Models;

internal class CollectionTableModelBuilder<TViewModel> : ITableBuilder<TViewModel>, ITableRowsBuilder<TViewModel>
{
    private readonly CollectionTableModel _collectionTableModel;

    public CollectionTableModelBuilder(CollectionTableModel collectionTableModel)
    {
        _collectionTableModel = collectionTableModel;
    }

    public ITableRowsBuilder<TViewModel> ToTable(ElementSelector selector)
    {
        _collectionTableModel.TableSelector = selector;
        return this;
    }

    public IModelBuilder<TViewModel> RowsFrom(RowSelector rowSelection)
    {
        var modelBuilder = new ModelBuilder<TViewModel>();

        _collectionTableModel.ModelBuilderData = modelBuilder;
        _collectionTableModel.RowSelector = rowSelection;

        return modelBuilder;
    }
}
