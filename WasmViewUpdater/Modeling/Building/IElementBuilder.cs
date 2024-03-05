using Vitraux.Modeling.Building.Elements;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Building;

public interface IElementBuilder<TViewModel, TSelector> where TSelector : ElementSelector
{
    IElementAttributeBuilder<TViewModel, TSelector> ToElement(TSelector selector);
    IELementContentBuilder<TViewModel, TSelector> ToContainerElement(TSelector selector);
}
