using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    internal interface IFromTemplateAppendToElementSelectorBuilder
    {
        FromTemplateAppendToElementSelector ById(string elementId);
        FromTemplateAppendToElementSelector ByQuerySelector(string querySelector);
    }
}