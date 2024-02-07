namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    internal record class ElementTemplateSelector : ElementSelector
    {
        internal ElementTemplateSelector(string templateId, ElementSelector elementToAppend)
            : base(ElementSelection.Template, templateId)
        {
            ElementToAppend = elementToAppend;
            TargetChildElement = new NoChildTemplateChildElementSelector();
        }

        public ElementSelector ElementToAppend { get; }
        public TemplateChildElementSelector TargetChildElement { get; set; }
    }
}
