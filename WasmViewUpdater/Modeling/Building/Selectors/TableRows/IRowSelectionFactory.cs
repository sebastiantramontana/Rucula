namespace WasmViewUpdater.Modeling.Building.Selectors.TableRows
{
    public interface IRowSelectionFactory
    {
        IRowSelection FromTemplate(string templateId);
        IRowSelection CopyFirstRow();
        IRowSelection CopyLasttRow();
    }
}
