namespace WasmViewUpdater.Model.Building.Selectors.Elements
{
    public interface IElementSelectorFactory
    {
        IElementSelector ById(string id);
        IFromTemplateSelector FromTemplate(string templateId);
        IElementSelector ByQuerySelector(string querySelector);
    }

    public interface IFromTemplateSelector
    {
        IElementSelectorFactory AddTo(IElementSelector selector);
    }
}
