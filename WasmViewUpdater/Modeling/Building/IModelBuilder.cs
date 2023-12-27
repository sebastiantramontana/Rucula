namespace Vitraux.Modeling.Building;

public interface IModelBuilder<TViewModel> : IModelBuilderData
{
    IElementBuilder<TViewModel> Value<TReturn>(Func<TViewModel, TReturn> func);
    ITableBuilder<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func);
}
