namespace WasmViewUpdater.Modeling.Building.Selectors.Elements.Builders
{
    internal class FromTemplateSelectorBuilder : IFromTemplateSelectorBuilder
    {
        private readonly string _templateId;

        internal FromTemplateSelectorBuilder(string templateId)
        {
            _templateId = templateId;
        }

        public IElementSelectorBuilder AppendTo(ElementSelector selector)
        {
            var templateElementSelector = new ElementTemplateSelector(_templateId, selector);
            return new ElementSelectorBuilder(templateElementSelector);
        }
    }
}
