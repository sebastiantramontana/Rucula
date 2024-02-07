namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public abstract record class TemplateChildElementSelector
    {
        private protected TemplateChildElementSelector(TemplateChildElementSelection selectionBy, string value)
        {
            SelectionBy = selectionBy;
            Value = value;
        }

        internal TemplateChildElementSelection SelectionBy { get; }
        internal string Value { get; }
    }
}
