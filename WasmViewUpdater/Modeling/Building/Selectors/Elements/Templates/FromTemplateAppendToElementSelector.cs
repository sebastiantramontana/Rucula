namespace Vitraux.Modeling.Building.Selectors.Elements.Templates
{
    public abstract record class FromTemplateAppendToElementSelector
    {
        private protected FromTemplateAppendToElementSelector(FromTemplateAppendToElementSelection selectionBy, string value)
        {
            SelectionBy = selectionBy;
            Value = value;
        }

        internal FromTemplateAppendToElementSelection SelectionBy { get; }
        internal string Value { get; }
    }
}
