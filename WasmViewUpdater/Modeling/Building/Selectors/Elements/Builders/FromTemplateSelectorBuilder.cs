using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.Modeling.Building.Selectors.Elements.Builders
{
    internal class AppendToTemplateSelectorBuilder : IAppendToTemplateSelectorBuilder
    {
        private readonly string _templateId;

        internal AppendToTemplateSelectorBuilder(string templateId)
        {
            _templateId = templateId;
        }

        public IToChildTemplateSelectorBuilder AppendTo(FromTemplateAppendToElementSelector appendToSelector)
        {
            var templateElementSelector = new ElementTemplateSelector(_templateId, appendToSelector);
            return new ToChildTemplateSelectorBuilder(templateElementSelector);
        }
    }
}
