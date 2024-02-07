using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    internal class TemplateChildElementSelectorBuilder : ITemplateChildElementSelectorBuilder
    {
        public TemplateChildElementSelector NoChild()
            => new NoChildTemplateChildElementSelector();

        public TemplateChildElementSelector ById(string elementId)
            => new TemplateChildElementIdSelector(elementId);

        public TemplateChildElementSelector ByQuerySelector(string querySelector)
            => new TemplateChildElementQuerySelector(querySelector);
    }
}
