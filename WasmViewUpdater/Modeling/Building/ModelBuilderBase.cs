using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal abstract class ModelBuilderBase<TViewModel, TFinalizable> : IFinalizableModelBuilder<TViewModel, TFinalizable>
{
    private readonly ICollection<ValueModel> _values;
    private readonly ICollection<CollectionTableModel> _collections;

    protected ModelBuilderBase()
    {
        _values = new List<ValueModel>();
        _collections = new List<CollectionTableModel>();
    }

    IEnumerable<ValueModel> IModelBuilderData.Values => _values;
    IEnumerable<CollectionTableModel> IModelBuilderData.CollectionTables => _collections;

    public ITableBuilder<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
    {
        var newCollection = new CollectionTableModel(func);
        _collections.Add(newCollection);

        return new TableBuilder<TReturn>(newCollection);
    }

    public IElementBuilder<TFinalizable> Value<TReturn>(Func<TViewModel, TReturn> func)
    {
        var newValueModel = new ValueModel(func);
        _values.Add(newValueModel);

        return CreateElementBuilder(newValueModel);
    }

    protected abstract IElementBuilder<TFinalizable> CreateElementBuilder(ValueModel valueModel);
}
