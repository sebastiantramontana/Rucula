using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Elements;

internal class ToFinalizableValueElementModel
    : IToContainerElementModel<IFinalizableValueModel>, IToElementModel<IFinalizableValueModel>
{
    private readonly TargetElement _targetElement;

    public ToFinalizableValueElementModel(TargetElement targetElement)
    {
        _targetElement = targetElement;
    }

    public IFinalizableValueModel ToAttribute(string attribute)
    {
        _targetElement.Place = new ElementPlace(ElementPlacing.Attribute, attribute);
        return new FinalizableValueModel(_targetElement.Parent);
    }

    public IFinalizableValueModel ToContent()
    {
        _targetElement.Place = new ElementPlace(ElementPlacing.Content);
        return new FinalizableValueModel(_targetElement.Parent);
    }
}

