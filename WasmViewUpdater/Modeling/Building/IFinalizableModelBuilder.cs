using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building
{
    public interface IFinalizableModelBuilder<TEntity, TFinalizable>
    {
        internal IEnumerable<ValueModel> Values { get; }
        internal IEnumerable<CollectionTableModel> CollectionTables { get; }
        IBuildingValueModel<TFinalizable> Value<TReturn>(Func<TEntity, TReturn> func);
        IBuildingCollectionModel<TReturn> Collection<TReturn>(Func<TEntity, IEnumerable<TReturn>> func);
    }
}
