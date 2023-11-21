namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    public interface IElementSelectorFactory
    {
        IElementSelector ById(string id);
        IFromTemplateSelector FromTemplate(string templateId);
        IElementSelector ByQuerySelector(string querySelector);
    }
}
