namespace WasmViewUpdater.Modeling.Building.Selectors.TableRows
{
    internal record class TemplateRowSelector(string templateId)
        : RowSelector(RowSelection.FromTemplate, templateId);
}
