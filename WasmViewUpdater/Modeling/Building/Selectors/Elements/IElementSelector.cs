namespace WasmViewUpdater.Modeling.Building.Selectors.Elements
{
    public interface IElementSelector
    {
        internal ElementSelection SelectionBy { get; }
        internal string Value { get; }
    }
}
