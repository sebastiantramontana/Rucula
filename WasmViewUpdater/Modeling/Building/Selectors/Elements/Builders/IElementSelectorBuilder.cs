namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    public interface IElementSelectorBuilder
    {
        ElementSelector ById(string elementId);
        IAppendToTemplateSelectorBuilder FromTemplate(string templateId);
        ElementSelector ByQuerySelector(string querySelector);
    }
}
