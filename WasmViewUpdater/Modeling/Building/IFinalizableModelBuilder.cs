using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

public interface IFinalizableModelBuilder<TViewModel, TFinalizable>
{
    internal IEnumerable<ValueModel> Values { get; }
    internal IEnumerable<CollectionTableModel> CollectionTables { get; }
    IBuildingValueModel<TFinalizable> Value<TReturn>(Func<TViewModel, TReturn> func);
    IBuildingCollectionModel<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func);
}
