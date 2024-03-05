using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Building.Elements
{

    public interface IElementAttributeBuilder<TViewModel, TSelector> where TSelector : ElementSelector
    {
        IFinalizableBuilder<TViewModel, TSelector> ToAttribute(string attribute);
    }
}
