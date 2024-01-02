using Vitraux.Modeling.Building.Elements;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building;

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
        => AddNewTargetElement(selector);

    public IELementContentBuilder<TViewModel> ToContainerElement(ElementSelector selector)
        => AddNewTargetElement(selector);

    public IFinalizableBuilder<TViewModel> ToAttribute(string attribute)
        => CreateFinalizableBuilder(new AttributeElementPlace(attribute));

    public IFinalizableBuilder<TViewModel> ToContent()
        => CreateFinalizableBuilder(new ContentElementPlace());

    private IFinalizableBuilder<TViewModel> CreateFinalizableBuilder(ElementPlace elementPlace)
    {
        _currentTargetElement.Place = elementPlace;
        return new FinalizableValueModelBuilder<TViewModel>(_modelBuilder, this);
    }

    private IELementContentBuilder<TViewModel> AddNewTargetElement(ElementSelector selector)
    {
        _currentTargetElement = new TargetElement(selector);
        _valueModel.TargetElements = _valueModel.TargetElements.Append(_currentTargetElement);
        return this;
    }
}
