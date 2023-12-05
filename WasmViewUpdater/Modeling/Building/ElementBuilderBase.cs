using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal abstract class ElementBuilderBase<TFinalizable> : IElementBuilder<TFinalizable>
{
    private readonly ValueModel _valueModel;

    protected ElementBuilderBase(ValueModel valueModel)
    {
        _valueModel = valueModel;
    }

    public IELementContentBuilder<TFinalizable> ToContainerElement(ElementSelector selector)
        => CreateELementContentBuilder(AddNewTargetElement(selector));

    public IElementAttributeBuilder<TFinalizable> ToElement(ElementSelector selector)
        => CreateElementAttributeBuilder(AddNewTargetElement(selector));

    private TargetElement AddNewTargetElement(ElementSelector selector)
    {
        var newTargetElement = new TargetElement(selector, _valueModel);
        _valueModel.TargetElements = _valueModel.TargetElements.Append(newTargetElement);

        return newTargetElement;
    }

    protected abstract IELementContentBuilder<TFinalizable> CreateELementContentBuilder(TargetElement targetElement);
    protected abstract IElementAttributeBuilder<TFinalizable> CreateElementAttributeBuilder(TargetElement targetElement);
}

