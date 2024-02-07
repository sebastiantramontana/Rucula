using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    internal interface ITemplateChildElementSelectorBuilder
    {
        TemplateChildElementSelector ById(string elementId);
        TemplateChildElementSelector ByQuerySelector(string querySelector);
        TemplateChildElementSelector NoChild();
    }
}