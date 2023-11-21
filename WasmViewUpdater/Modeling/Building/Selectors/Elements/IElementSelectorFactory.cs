namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    public interface IElementSelectorFactory
    {
        ElementSelector ById(string id);
        IFromTemplateSelector FromTemplate(string templateId);
        ElementSelector ByQuerySelector(string querySelector);
    }
}
