using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class ModelBuilder<TViewModel> : IModelBuilder<TViewModel>
{
    private readonly ICollection<ValueModel> _values;
    private readonly ICollection<CollectionTableModel> _collections;

    internal ModelBuilder()
    {
        _values = new List<ValueModel>();
        _collections = new List<CollectionTableModel>();
    }

    IEnumerable<ValueModel> IModelBuilderData.Values => _values;
    IEnumerable<CollectionTableModel> IModelBuilderData.CollectionTables => _collections;

    public IElementBuilder<TViewModel> Value<TReturn>(Func<TViewModel, TReturn> func)
    {
        var valueModel = new ValueModel(func);
        _values.Add(valueModel);

        return new ValueModelBuilder<TViewModel>(valueModel, this);
    }

    public ITableBuilder<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
    {
        var collectionTableModel = new CollectionTableModel(func);
        _collections.Add(collectionTableModel);

        return new CollectionTableModelBuilder<TReturn>(collectionTableModel);
    }
}
