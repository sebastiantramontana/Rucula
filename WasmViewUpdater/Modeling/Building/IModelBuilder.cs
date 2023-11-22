using WasmViewUpdater.Modeling.Building.Finalizables;

namespace WasmViewUpdater.Modeling.Building;

public interface IModelBuilder<TViewModel> : IFinalizableModelBuilder<TViewModel, IFinalizableValueModel>
{
}
