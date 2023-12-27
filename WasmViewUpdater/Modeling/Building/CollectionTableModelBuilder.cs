using Vitraux.Modeling.Building.Elements;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.TableRows;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building;

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
