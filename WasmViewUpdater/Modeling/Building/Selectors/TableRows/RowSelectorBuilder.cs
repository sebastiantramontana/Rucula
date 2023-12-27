namespace Vitraux.Modeling.Building.Selectors.TableRows
{
    internal class RowSelectorBuilder : IRowSelectorBuilder
    {
        public RowSelector FromTemplate(string templateId)
            => new TemplateRowSelector(templateId);
    }
}
