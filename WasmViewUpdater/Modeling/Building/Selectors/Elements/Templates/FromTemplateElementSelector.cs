namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public abstract record class FromTemplateElementSelector
    {
        private protected FromTemplateElementSelector(FromTemplateElementSelection selectionBy, string value)
        {
            SelectionBy = selectionBy;
            Value = value;
        }

        internal FromTemplateElementSelection SelectionBy { get; }
        internal string Value { get; }
    }
}
