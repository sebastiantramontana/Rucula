namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    public interface IFromTemplateSelector
    {
        IElementSelectorFactory AddTo(IElementSelector selector);
    }
}
