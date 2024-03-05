namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public record class FromTemplateAppendToElementIdSelector : FromTemplateAppendToElementSelector
    {
        internal FromTemplateAppendToElementIdSelector(string id)
            : base(FromTemplateAppendToElementSelection.Id, id)
        {
        }
    }
}
