namespace WasmViewUpdater.Modeling.Building.Selectors.Elements.Builders
{
    internal class ElementSelectorBuilder : IElementSelectorBuilder
    {
        private readonly ElementSelector _parent;

        public ElementSelectorBuilder(ElementSelector parent)
            => _parent = parent;

        public ElementSelector ByElementObject(string objectName)
            => new ElementObjectSelector(objectName, _parent);

        public ElementSelector ById(string elementId)
            => new ElementIdSelector(elementId, _parent);

        public ElementSelector ByQuerySelector(string querySelector)
            => new ElementQuerySelector(querySelector, _parent);

        public IFromTemplateSelectorBuilder FromTemplate(string templateId)
            => new FromTemplateSelectorBuilder(templateId);
    }
}
