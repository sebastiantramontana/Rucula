using Vitraux.Modeling.Building.Elements;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Building;

public interface IElementBuilder<TViewModel>
{
    IElementAttributeBuilder<TViewModel> ToElement(ElementSelector selector);
    IELementContentBuilder<TViewModel> ToContainerElement(ElementSelector selector);
}
