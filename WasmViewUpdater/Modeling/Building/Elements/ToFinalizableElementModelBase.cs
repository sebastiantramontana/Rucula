using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Elements;

internal abstract class ToFinalizableElementModelBase<TFinalizable>
    : IToContainerElementModel<TFinalizable>, IToElementModel<TFinalizable>
{
    private readonly TargetElement _targetElement;

    protected ToFinalizableElementModelBase(TargetElement targetElement)
    {
        _targetElement = targetElement;
    }

    public TFinalizable ToAttribute(string attribute)
        => CreateFinalizable(new ElementPlace(ElementPlacing.Attribute, attribute));

    public TFinalizable ToContent()
        => CreateFinalizable(new ElementPlace(ElementPlacing.Content));

    private TFinalizable CreateFinalizable(ElementPlace place)
    {
        _targetElement.Place = place;
        return CreateFinalizable(_targetElement.Parent);
    }

    protected abstract TFinalizable CreateFinalizable(ValueModel parent);
}

