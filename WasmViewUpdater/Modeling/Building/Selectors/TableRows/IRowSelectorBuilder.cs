namespace WasmViewUpdater.Modeling.Building.Selectors.TableRows
{
    public interface IRowSelectorBuilder
    {
        RowSelector FromTemplate(string templateId);
    }
}
