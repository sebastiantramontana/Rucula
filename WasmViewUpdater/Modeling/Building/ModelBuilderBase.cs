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

    IEnumerable<ValueModel> IFinalizableModelBuilder<TViewModel, TFinalizable>.Values => _values;
    IEnumerable<CollectionTableModel> IFinalizableModelBuilder<TViewModel, TFinalizable>.CollectionTables => _collections;

    public IBuildingCollectionModel<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
    {
        _collections.Add(new CollectionTableModel(func));

        return default!;
    }

    public IBuildingValueModel<TFinalizable> Value<TReturn>(Func<TViewModel, TReturn> func)
    {
        var newValueModel = new ValueModel(func);
        _values.Add(newValueModel);

        return CreateBuildingValueModel(newValueModel);
    }

    protected abstract IBuildingValueModel<TFinalizable> CreateBuildingValueModel(ValueModel valueModel);
}
