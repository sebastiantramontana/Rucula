namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public record class FromTemplateElementQuerySelector : FromTemplateElementSelector
    {
        public FromTemplateElementQuerySelector(string querySelector)
            : base(FromTemplateElementSelection.QuerySelector, querySelector)
        {
        }
    }
}
