namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public record class TemplateChildElementQuerySelector : TemplateChildElementSelector
    {
        public TemplateChildElementQuerySelector(string querySelector)
            : base(TemplateChildElementSelection.QuerySelector, querySelector)
        {
        }
    }
}
