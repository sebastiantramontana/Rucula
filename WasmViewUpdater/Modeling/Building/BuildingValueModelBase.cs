using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal abstract class BuildingValueModelBase<TFinalizable> : IBuildingValueModel<TFinalizable>
{
    private readonly ValueModel _valueModel;

    protected BuildingValueModelBase(ValueModel valueModel)
    {
        _valueModel = valueModel;
    }

    public IToContainerElementModel<TFinalizable> ToContainerElement(ElementSelector selector)
        => CreateToContainerElementModel(AddNewTargetElement(selector));

    public IToElementModel<TFinalizable> ToElement(ElementSelector selector)
        => CreateToElementModel(AddNewTargetElement(selector));

    private TargetElement AddNewTargetElement(ElementSelector selector)
    {
        var newTargetElement = new TargetElement(selector, _valueModel);
        _valueModel.TargetElements = _valueModel.TargetElements.Append(newTargetElement);

        return newTargetElement;
    }

    protected abstract IToContainerElementModel<TFinalizable> CreateToContainerElementModel(TargetElement targetElement);
    protected abstract IToElementModel<TFinalizable> CreateToElementModel(TargetElement targetElement);
}

