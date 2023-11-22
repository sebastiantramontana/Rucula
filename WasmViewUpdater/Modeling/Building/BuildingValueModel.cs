using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class BuildingValueModel : BuildingValueModelBase<IFinalizableValueModel>, IBuildingValueModel<IFinalizableValueModel>
{
    public BuildingValueModel(ValueModel valueModel)
        : base(valueModel)
    {
    }

    protected override IToContainerElementModel<IFinalizableValueModel> CreateToContainerElementModel(TargetElement targetElement)
        => new ToFinalizableValueElementModel(targetElement);

    protected override IToElementModel<IFinalizableValueModel> CreateToElementModel(TargetElement targetElement)
        => new ToFinalizableValueElementModel(targetElement);
}

