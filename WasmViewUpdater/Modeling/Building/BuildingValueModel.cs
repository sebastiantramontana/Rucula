using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class BuildingValueModel : IBuildingValueModel<IFinalizableValueModel>
{
    private readonly ValueModel _valueModel;

    public BuildingValueModel(ValueModel valueModel)
    {
        _valueModel = valueModel;
    }

    public IToContainerElementModel<IFinalizableValueModel> ToContainerElement(ElementSelector selector)
        => new ToFinalizableValueElementModel(AddNewTargetElement(selector));

    public IToElementModel<IFinalizableValueModel> ToElement(ElementSelector selector)
        => new ToFinalizableValueElementModel(AddNewTargetElement(selector));

    private TargetElement AddNewTargetElement(ElementSelector selector)
    {
        var newTargetElement = new TargetElement(selector, _valueModel);
        _valueModel.TargetElements = _valueModel.TargetElements.Append(newTargetElement);

        return newTargetElement;
    }
}

