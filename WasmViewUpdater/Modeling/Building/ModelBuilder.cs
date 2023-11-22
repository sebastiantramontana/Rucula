using WasmViewUpdater.Modeling.Building.Finalizables;
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

    IEnumerable<ValueModel> IFinalizableModelBuilder<TViewModel, IFinalizableValueModel>.Values => _values;
    IEnumerable<CollectionTableModel> IFinalizableModelBuilder<TViewModel, IFinalizableValueModel>.CollectionTables => _collections;

    public IBuildingCollectionModel<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
    {
        _collections.Add(new CollectionTableModel(func));

        return default!;
    }

    public IBuildingValueModel<IFinalizableValueModel> Value<TReturn>(Func<TViewModel, TReturn> func)
    {
        var newValueModel = new ValueModel(func);
        _values.Add(newValueModel);

        return new BuildingValueModel(newValueModel);
    }
}
