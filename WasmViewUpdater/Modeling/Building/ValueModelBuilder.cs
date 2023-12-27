using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Models;

internal class ValueModelBuilder<TViewModel> : IElementBuilder<TViewModel>, IELementContentBuilder<TViewModel>
{
    private readonly ValueModel _valueModel;
    private readonly IModelBuilder<TViewModel> _modelBuilder;

    private TargetElement _currentTargetElement = default!;

    public ValueModelBuilder(ValueModel valueModel, IModelBuilder<TViewModel> modelBuilder)
    {
        _valueModel = valueModel;
        _modelBuilder = modelBuilder;
    }

    public IElementAttributeBuilder<TViewModel> ToElement(ElementSelector selector)
    {
        _currentTargetElement = new TargetElement(selector, _valueModel);
        _valueModel.TargetElements = _valueModel.TargetElements.Append(_currentTargetElement);
        return this;
    }

    public IELementContentBuilder<TViewModel> ToContainerElement(ElementSelector selector)
    {
        _currentTargetElement = new TargetElement(selector, _valueModel);
        _valueModel.TargetElements = _valueModel.TargetElements.Append(_currentTargetElement);
        return this;
    }

    public IFinalizableBuilder<TViewModel> ToAttribute(string attribute)
    {
        _currentTargetElement.Place = new AttributeElementPlace(attribute);
        return new FinalizableValueModelBuilder<TViewModel>(_modelBuilder, this);
    }

    public IFinalizableBuilder<TViewModel> ToContent()
    {
        _currentTargetElement.Place = new ContentElementPlace();
        return new FinalizableValueModelBuilder<TViewModel>(_modelBuilder, this);
    }
}
