using WasmViewUpdater.Model.Building;

namespace WasmViewUpdater.Model
{

    public interface IModelConfiguration<T>
    {
        IEnumerable<IModel> Configure(IModelBuilder<T> modelBuilder);
    }
}
