using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    internal class FromTemplateElementSelectorBuilder : IFromTemplateElementSelectorBuilder
    {
        public FromTemplateElementSelector ById(string elementId)
            => new FromTemplateElementIdSelector(elementId);

        public FromTemplateElementSelector ByQuerySelector(string querySelector)
            => new FromTemplateElementQuerySelector(querySelector);
    }
}
