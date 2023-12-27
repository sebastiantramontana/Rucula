namespace Vitraux.Modeling.Building.Selectors.Elements
{
    internal record class ElementTemplateSelector : ElementSelector
    {
        internal ElementTemplateSelector(string templateId, ElementSelector elementToAppend)
            : base(ElementSelection.Template, templateId)
        {
            ElementToAppend = elementToAppend;
        }

        public ElementSelector ElementToAppend { get; }
        public ElementSelector TargetChildElement { get; set; } = default!;
    }
}
