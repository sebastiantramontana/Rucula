namespace WasmViewUpdater.Modeling.Building.Selectors.Elements.Builders
{
    internal class ElementSelectorBuilder : IElementSelectorBuilder
    {
        public ElementSelector ByElementObject(string objectName)
            => new ElementObjectSelector(objectName);

        public ElementSelector ById(string elementId)
            => new ElementIdSelector(elementId);

        public ElementSelector ByQuerySelector(string querySelector)
            => new ElementQuerySelector(querySelector);

        public IAppendToTemplateSelectorBuilder FromTemplate(string templateId)
            => new AppendToTemplateSelectorBuilder(templateId);
    }
}
