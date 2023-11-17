using WasmViewUpdater.Modeling.Building.Finalizables;

namespace WasmViewUpdater.Modeling.Building
{
    public interface IModelBuilder<TEntity> : IFinalizableModelBuilder<TEntity, IFinalizableValueModel>
    {
    }
}
