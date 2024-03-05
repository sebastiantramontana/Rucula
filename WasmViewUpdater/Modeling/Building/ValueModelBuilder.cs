using Vitraux.Modeling.Building.Elements;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building;

internal class ValueModelBuilder<TViewModel, TSelector> : IElementBuilder<TViewModel, TSelector>, IELementContentBuilder<TViewModel, TSelector> where TSelector : ElementSelector
{
    private readonly ValueModel _valueModel;
    private readonly IModelBuilder<TViewModel, TSelector> _modelBuilder;

    private TargetElement _currentTargetElement = default!;

    public ValueModelBuilder(ValueModel valueModel, IModelBuilder<TViewModel, TSelector> modelBuilder)
    {
        _valueModel = valueModel;
        _modelBuilder = modelBuilder;
    }

    public IElementAttributeBuilder<TViewModel, TSelector> ToElement(TSelector selector)
        => AddNewTargetElement(selector);

    public IELementContentBuilder<TViewModel, TSelector> ToContainerElement(TSelector selector)
        => AddNewTargetElement(selector);

    public IFinalizableBuilder<TViewModel, TSelector> ToAttribute(string attribute)
        => CreateFinalizableBuilder(new AttributeElementPlace(attribute));

    public IFinalizableBuilder<TViewModel, TSelector> ToContent()
        => CreateFinalizableBuilder(new ContentElementPlace());

    private IFinalizableBuilder<TViewModel, TSelector> CreateFinalizableBuilder(ElementPlace elementPlace)
    {
        _currentTargetElement.Place = elementPlace;
        return new FinalizableValueModelBuilder<TViewModel, TSelector>(_modelBuilder, this);
    }

    private IELementContentBuilder<TViewModel, TSelector> AddNewTargetElement(TSelector selector)
    {
        _currentTargetElement = new TargetElement(selector);
        _valueModel.TargetElements = _valueModel.TargetElements.Append(_currentTargetElement);
        return this;
    }
}
