namespace WasmViewUpdater.Model.Building.Selectors.TableRows
{
    public interface IRowSelectionFactory
    {
        IRowSelection FromTemplate(string templateId);
        IRowSelection CopyFirstRow();
        IRowSelection CopyLasttRow();
    }
}
