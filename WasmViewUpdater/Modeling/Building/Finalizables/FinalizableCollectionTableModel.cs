using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class FinalizableCollectionTableModel<TViewModel> : FinalizableTableModelBase<TViewModel>, IFinalizableCollectionTableModel<TViewModel>
{
    private readonly IBuildingValueModel<IFinalizableCollectionTableModel<TViewModel>> _buildingValueModel;

    public FinalizableCollectionTableModel(ValueModel valueModel)
    {
        _buildingValueModel = CreateBuildingValueModel(valueModel);
    }

    public IToContainerElementModel<IFinalizableCollectionTableModel<TViewModel>> ToContainerElement(ElementSelector selector)
        => _buildingValueModel.ToContainerElement(selector);

    public IToElementModel<IFinalizableCollectionTableModel<TViewModel>> ToElement(ElementSelector selector)
        => _buildingValueModel.ToElement(selector);
}

