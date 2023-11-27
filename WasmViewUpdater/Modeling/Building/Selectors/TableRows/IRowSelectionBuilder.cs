namespace WasmViewUpdater.Modeling.Building.Selectors.TableRows
{
    public interface IRowSelectionBuilder
    {
        IRowSelection FromTemplate(string templateId);
        IRowSelection CopyFirstRow();
        IRowSelection CopyLasttRow();
    }
}
