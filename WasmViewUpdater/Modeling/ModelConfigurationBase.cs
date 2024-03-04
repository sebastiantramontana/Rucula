using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Builders;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;
using Vitraux.Modeling.Building.Selectors.TableRows;

namespace Vitraux.Modeling
{
    public abstract class ModelConfigurationBase<TViewModel> : IModelConfiguration<TViewModel>
    {
        private IElementSelectorBuilder _elementSelectorBuilder = default!;
        private IRowSelectorBuilder _rowSelectionBuilder = default!;
        private IFromTemplateElementSelectorBuilder _fromTemplateElementSelectorBuilder = default!;

        void IModelConfiguration<TViewModel>.Configure(IModelBuilder<TViewModel> modelBuilder, IElementSelectorBuilder elementSelectorBuilder, IRowSelectorBuilder rowSelectionBuilder, IFromTemplateElementSelectorBuilder fromTemplateElementSelectorBuilder)
        {
            _elementSelectorBuilder = elementSelectorBuilder;
            _rowSelectionBuilder = rowSelectionBuilder;
            _fromTemplateElementSelectorBuilder = fromTemplateElementSelectorBuilder;

            Configure(modelBuilder);
        }

        protected ElementIdSelector ById(string id) => _elementSelectorBuilder.ById(id);
        protected ElementQuerySelector ByQuerySelector(string querySelector) => _elementSelectorBuilder.ByQuerySelector(querySelector);
        protected IAppendToTemplateSelectorBuilder FromTemplate(string templateId) => _elementSelectorBuilder.FromTemplate(templateId);
        protected RowSelector Template(string templateId) => _rowSelectionBuilder.FromTemplate(templateId);
        protected FromTemplateElementSelector FromTemplateById(string id) => _fromTemplateElementSelectorBuilder.ById(id);
        protected FromTemplateElementSelector FromTemplateByQuerySelector(string querySelector) => _fromTemplateElementSelectorBuilder.ByQuerySelector(querySelector);
        protected abstract void Configure(IModelBuilder<TViewModel> modelBuilder);
    }
}
