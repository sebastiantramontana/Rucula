namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    public record class ElementSelector
    {
        internal ElementSelector(ElementSelection selectionBy, string value, string parent)
        {
            SelectionBy = selectionBy;
            Value = value;
            Parent = parent;
        }

        internal ElementSelection SelectionBy { get; }
        internal string Value { get; } = string.Empty;
        internal string Parent { get; } = string.Empty;
    }
}
