using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Builders;
using Vitraux.Modeling.Building.Selectors.TableRows;

namespace Vitraux.Modeling
{
    public abstract class ModelConfigurationBase<TViewModel> : IModelConfiguration<TViewModel>
    {
        private IElementSelectorBuilder _elementSelectorBuilder = default!;
        private IRowSelectorBuilder _rowSelectionBuilder = default!;

        void IModelConfiguration<TViewModel>.Configure(IModelBuilder<TViewModel> modelBuilder, IElementSelectorBuilder elementSelectorBuilder, IRowSelectorBuilder rowSelectionBuilder)
        {
            _elementSelectorBuilder = elementSelectorBuilder;
            _rowSelectionBuilder = rowSelectionBuilder;

            Configure(modelBuilder);
        }

        protected ElementSelector ById(string id) => _elementSelectorBuilder.ById(id);
        protected IAppendToTemplateSelectorBuilder FromTemplate(string templateId) => _elementSelectorBuilder.FromTemplate(templateId);
        protected ElementSelector ByQuerySelector(string querySelector) => _elementSelectorBuilder.ByQuerySelector(querySelector);
        protected RowSelector Template(string templateId) => _rowSelectionBuilder.FromTemplate(templateId);
        protected abstract void Configure(IModelBuilder<TViewModel> modelBuilder);
    }
}
