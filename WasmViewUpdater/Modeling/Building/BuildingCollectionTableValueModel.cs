using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class BuildingCollectionTableValueModel<TViewModel> : BuildingValueModelBase<IFinalizableCollectionTableModel<TViewModel>>, IBuildingValueModel<IFinalizableCollectionTableModel<TViewModel>>
{
    public BuildingCollectionTableValueModel(ValueModel valueModel)
        : base(valueModel)
    {
    }

    protected override IToContainerElementModel<IFinalizableCollectionTableModel<TViewModel>> CreateToContainerElementModel(TargetElement targetElement)
        => new ToFinalizableCollectionTableElementModel<TViewModel>(targetElement);

    protected override IToElementModel<IFinalizableCollectionTableModel<TViewModel>> CreateToElementModel(TargetElement targetElement)
        => new ToFinalizableCollectionTableElementModel<TViewModel>(targetElement);
}

