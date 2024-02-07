namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public record class TemplateChildElementIdSelector : TemplateChildElementSelector
    {
        public TemplateChildElementIdSelector(string id)
            : base(TemplateChildElementSelection.Id, id)
        {
        }
    }
}
