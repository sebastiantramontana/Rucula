using WasmViewUpdater.Modeling.Building;

namespace WasmViewUpdater.Modeling
{

    public interface IModelConfiguration<TEntity>
    {
        IEnumerable<IModel> Configure(IModelBuilder<TEntity> modelBuilder);
    }
}
