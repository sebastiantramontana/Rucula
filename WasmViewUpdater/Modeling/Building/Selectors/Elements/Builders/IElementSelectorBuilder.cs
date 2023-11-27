namespace WasmViewUpdater.Modeling.Building.Selectors.Elements.Builders
{
    public interface IElementSelectorBuilder
    {
        ElementSelector ById(string id);
        IFromTemplateSelectorBuilder FromTemplate(string templateId);
        ElementSelector ByQuerySelector(string querySelector);
        ElementSelector ByElementObject(string objectName);
    }
}
