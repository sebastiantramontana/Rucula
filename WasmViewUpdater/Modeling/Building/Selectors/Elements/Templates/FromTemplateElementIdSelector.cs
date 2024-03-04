namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public record class FromTemplateElementIdSelector : FromTemplateElementSelector
    {
        internal FromTemplateElementIdSelector(string id)
            : base(FromTemplateElementSelection.Id, id)
        {
        }
    }
}
