namespace WasmViewUpdater.Modeling.Building
{
    public interface IFinalizableModelBuilder<TEntity, TFinalizable>
        where TFinalizable : IModel
    {
        IBuildingValueModel<TFinalizable> Value<TReturn>(Func<TEntity, TReturn> func);
        IBuildingCollectionModel<TReturn> Collection<TReturn>(Func<TEntity, IEnumerable<TReturn>> func);
    }
}
