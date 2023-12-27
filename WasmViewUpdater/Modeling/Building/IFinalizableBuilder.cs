namespace WasmViewUpdater.Modeling.Building
{
    public interface IFinalizableBuilder<TViewModel> : IModelBuilder<TViewModel>, IElementBuilder<TViewModel>
    {
    }
}
