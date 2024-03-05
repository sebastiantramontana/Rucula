namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    internal record class ElementTemplateSelector : ElementSelector
    {
        internal ElementTemplateSelector(string templateId, FromTemplateAppendToElementSelector elementToAppend)
            : base(ElementSelection.Template, templateId)
        {
            ElementToAppend = elementToAppend;
        }

        public FromTemplateAppendToElementSelector ElementToAppend { get; }
        public ElementQuerySelector TargetChildElement { get; set; } = default!;
    }
}
