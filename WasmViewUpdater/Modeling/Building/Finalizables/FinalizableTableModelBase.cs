using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Finalizables;

internal abstract class FinalizableTableModelBase<TViewModel> : ModelBuilderBase<TViewModel, IFinalizableCollectionTableModel<TViewModel>>
{
    protected override IBuildingValueModel<IFinalizableCollectionTableModel<TViewModel>> CreateBuildingValueModel(ValueModel valueModel)
        => new BuildingCollectionTableValueModel<TViewModel>(valueModel);
}

