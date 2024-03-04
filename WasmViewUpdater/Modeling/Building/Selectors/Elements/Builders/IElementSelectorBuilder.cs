namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    public interface IElementSelectorBuilder
    {
        ElementIdSelector ById(string elementId);
        IAppendToTemplateSelectorBuilder FromTemplate(string templateId);
        ElementQuerySelector ByQuerySelector(string querySelector);
    }
}
