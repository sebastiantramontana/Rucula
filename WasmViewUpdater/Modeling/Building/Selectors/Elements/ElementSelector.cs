namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    public abstract record class ElementSelector
    {
        internal static readonly ElementSelector DocumentElement = new ElementObjectSelector("document", null);

        private protected ElementSelector(ElementSelection selectionBy, string value, ElementSelector? parent)
        {
            SelectionBy = selectionBy;
            Value = value;
            Parent = parent;
        }

        internal ElementSelection SelectionBy { get; }
        internal string Value { get; } = string.Empty;
        internal ElementSelector? Parent { get; }
    }
}
