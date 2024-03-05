using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Building.Elements
{
    public interface IELementContentBuilder<TViewModel, TSelector> : IElementAttributeBuilder<TViewModel, TSelector> where TSelector : ElementSelector
    {
        IFinalizableBuilder<TViewModel, TSelector> ToContent();
    }
}
