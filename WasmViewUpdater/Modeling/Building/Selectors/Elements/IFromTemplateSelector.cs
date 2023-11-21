namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    public interface IFromTemplateSelector
    {
        IElementSelectorFactory AddTo(ElementSelector selector);
    }
}
