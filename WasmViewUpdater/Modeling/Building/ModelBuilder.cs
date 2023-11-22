using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class ModelBuilder<TViewModel> : ModelBuilderBase<TViewModel, IFinalizableValueModel>, IModelBuilder<TViewModel>
{
    protected override IBuildingValueModel<IFinalizableValueModel> CreateBuildingValueModel(ValueModel valueModel)
        => new BuildingValueModel(valueModel);
}
