namespace WasmViewUpdater.Modeling.Building.Selectors.TableRows
{
    internal class RowSelectionBuilder : IRowSelectionBuilder
    {
        public RowSelector FromTemplate(string templateId)
            => new TemplateRowSelector(templateId);
    }
}
