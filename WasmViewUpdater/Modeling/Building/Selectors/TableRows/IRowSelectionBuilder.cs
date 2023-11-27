namespace WasmViewUpdater.Modeling.Building.Selectors.TableRows
{
    public interface IRowSelectionBuilder
    {
        RowSelector FromTemplate(string templateId);
    }
}
