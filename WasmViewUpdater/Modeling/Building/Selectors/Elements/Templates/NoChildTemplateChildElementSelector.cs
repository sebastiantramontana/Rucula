namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public record class NoChildTemplateChildElementSelector : TemplateChildElementSelector
    {
        public NoChildTemplateChildElementSelector()
            : base(TemplateChildElementSelection.NoChild, string.Empty)
        {
        }
    }
}
