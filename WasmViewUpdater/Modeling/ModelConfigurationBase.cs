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
        private ITemplateChildElementSelectorBuilder _templateChildElementSelectorBuilder = default!;

        void IModelConfiguration<TViewModel>.Configure(IModelBuilder<TViewModel> modelBuilder, IElementSelectorBuilder elementSelectorBuilder, IRowSelectorBuilder rowSelectionBuilder, ITemplateChildElementSelectorBuilder templateChildElementSelectorBuilder)
        {
            _elementSelectorBuilder = elementSelectorBuilder;
            _rowSelectionBuilder = rowSelectionBuilder;
            _templateChildElementSelectorBuilder = templateChildElementSelectorBuilder;

            Configure(modelBuilder);
        }

        protected ElementSelector ById(string id) => _elementSelectorBuilder.ById(id);
        protected ElementSelector ByQuerySelector(string querySelector) => _elementSelectorBuilder.ByQuerySelector(querySelector);
        protected IAppendToTemplateSelectorBuilder FromTemplate(string templateId) => _elementSelectorBuilder.FromTemplate(templateId);
        protected RowSelector Template(string templateId) => _rowSelectionBuilder.FromTemplate(templateId);
        protected TemplateChildElementSelector NoChild => _templateChildElementSelectorBuilder.NoChild();
        protected TemplateChildElementSelector TemplateChildById(string id) => _templateChildElementSelectorBuilder.ById(id);
        protected TemplateChildElementSelector TemplateChildByQuerySelector(string querySelector) => _templateChildElementSelectorBuilder.ByQuerySelector(querySelector);
        protected abstract void Configure(IModelBuilder<TViewModel> modelBuilder);
    }
}
