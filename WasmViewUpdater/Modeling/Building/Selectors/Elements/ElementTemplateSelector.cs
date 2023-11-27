namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    internal record class ElementTemplateSelector : ElementSelector
    {
        internal ElementTemplateSelector(string templateId, ElementSelector elementToAppend)
            : base(ElementSelection.Template, templateId, DocumentElement)
        {
            ElementToAppend = elementToAppend;
        }

        public ElementSelector ElementToAppend { get; }
    }
}
