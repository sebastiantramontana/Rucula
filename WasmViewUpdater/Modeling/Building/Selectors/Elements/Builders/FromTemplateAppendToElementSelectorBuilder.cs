using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    internal class FromTemplateAppendToElementSelectorBuilder : IFromTemplateAppendToElementSelectorBuilder
    {
        public FromTemplateAppendToElementSelector ById(string elementId)
            => new FromTemplateAppendToElementIdSelector(elementId);

        public FromTemplateAppendToElementSelector ByQuerySelector(string querySelector)
            => new FromTemplateAppendToElementQuerySelector(querySelector);
    }
}
