using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Building
{
    public interface IFinalizableBuilder<TViewModel, TSelector> : IModelBuilder<TViewModel, TSelector>, IElementBuilder<TViewModel, TSelector> where TSelector : ElementSelector
    {
    }
}
