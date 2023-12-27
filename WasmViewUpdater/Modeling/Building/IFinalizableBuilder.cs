namespace Vitraux.Modeling.Building
{
    public interface IFinalizableBuilder<TViewModel> : IModelBuilder<TViewModel>, IElementBuilder<TViewModel>
    {
    }
}
