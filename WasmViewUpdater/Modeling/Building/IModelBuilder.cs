using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Building;

public interface IModelBuilder<TViewModel, TSelector> : IModelBuilderData where TSelector : ElementSelector
{
    IElementBuilder<TViewModel, TSelector> Value<TReturn>(Func<TViewModel, TReturn> func);
    ITableBuilder<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func);
}

public interface IModelBuilder<TViewModel> : IModelBuilder<TViewModel, ElementSelector>
{
}
