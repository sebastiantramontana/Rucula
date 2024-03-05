namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public record class FromTemplateAppendToElementQuerySelector : FromTemplateAppendToElementSelector
    {
        public FromTemplateAppendToElementQuerySelector(string querySelector)
            : base(FromTemplateAppendToElementSelection.QuerySelector, querySelector)
        {
        }
    }
}
