namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    internal record class ElementTemplateSelector : ElementSelector
    {
        internal ElementTemplateSelector(string templateId, FromTemplateElementSelector elementToAppend)
            : base(ElementSelection.Template, templateId)
        {
            ElementToAppend = elementToAppend;
        }

        public FromTemplateElementSelector ElementToAppend { get; }
        public ElementQuerySelector TargetChildElement { get; set; } = default!;
    }
}
