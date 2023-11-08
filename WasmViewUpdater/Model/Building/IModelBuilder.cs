using WasmViewUpdater.Model.Building.Finalizables;

namespace WasmViewUpdater.Model.Building
{
    public interface IModelBuilder<TEntity, TFinalizable>
        where TFinalizable : IModel
    {
        IValueModel<TFinalizable> Value<TReturn>(Func<TEntity, TReturn> func);
        ICollectionModel<TReturn> Collection<TReturn>(Func<TEntity, IEnumerable<TReturn>> func);
    }

    public interface IModelBuilder<TEntity> : IModelBuilder<TEntity, IFinalizableValueModel>
    {
    }
}
