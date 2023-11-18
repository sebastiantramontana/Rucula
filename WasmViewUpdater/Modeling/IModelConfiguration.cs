using WasmViewUpdater.Modeling.Building;

namespace WasmViewUpdater.Modeling
{

    public interface IModelConfiguration<TEntity>
    {
        IModelBuilder<TEntity> Configure(IModelBuilder<TEntity> modelBuilder);
    }
}
