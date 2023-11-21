using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class ModelBuilder<TEntity> : IModelBuilder<TEntity>
{
    private readonly ICollection<ValueModel> _values;
    private readonly ICollection<CollectionTableModel> _collections;

    internal ModelBuilder()
    {
        _values = new List<ValueModel>();
        _collections = new List<CollectionTableModel>();
    }

    IEnumerable<ValueModel> IFinalizableModelBuilder<TEntity, IFinalizableValueModel>.Values => _values;
    IEnumerable<CollectionTableModel> IFinalizableModelBuilder<TEntity, IFinalizableValueModel>.CollectionTables => _collections;

    public IBuildingCollectionModel<TReturn> Collection<TReturn>(Func<TEntity, IEnumerable<TReturn>> func)
    {
        _collections.Add(new CollectionTableModel(func));

        return default!;
    }

    public IBuildingValueModel<IFinalizableValueModel> Value<TReturn>(Func<TEntity, TReturn> func)
    {
        var newValueModel = new ValueModel(func);
        _values.Add(newValueModel);

        return new BuildingValueModel(newValueModel);
    }
}
