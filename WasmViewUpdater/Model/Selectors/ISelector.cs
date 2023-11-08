namespace WasmViewUpdater.Model.Selectors
{
    public interface ISelector
    {
        internal ElementSelection SelectionBy { get; }
        internal string Value { get; }
    }
}
