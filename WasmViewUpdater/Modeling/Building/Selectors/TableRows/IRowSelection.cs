namespace WasmViewUpdater.Modeling.Building.Selectors.TableRows
{
    public interface IRowSelection
    {
        internal RowSelectionFrom From { get; }
        internal string? Value { get; }
    }
}
