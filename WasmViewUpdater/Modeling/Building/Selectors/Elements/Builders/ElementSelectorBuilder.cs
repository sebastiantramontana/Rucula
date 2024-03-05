using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    internal class ElementSelectorBuilder : IElementSelectorBuilder
    {
        public ElementIdSelector ById(string elementId)
            => new(elementId);

        public ElementQuerySelector ByQuerySelector(string querySelector)
            => new(querySelector);

        public IAppendToTemplateSelectorBuilder FromTemplate(string templateId)
            => new AppendToTemplateSelectorBuilder(templateId);
    }
}
