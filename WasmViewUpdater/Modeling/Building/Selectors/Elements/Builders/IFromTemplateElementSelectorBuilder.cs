using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    internal interface IFromTemplateElementSelectorBuilder
    {
        FromTemplateElementSelector ById(string elementId);
        FromTemplateElementSelector ByQuerySelector(string querySelector);
    }
}