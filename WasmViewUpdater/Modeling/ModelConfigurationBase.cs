using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements.Builders;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling
{
    public abstract class ModelConfigurationBase<TViewModel> : IModelConfiguration<TViewModel>
    {
        private IElementSelectorBuilder _elementSelectorBuilder = default!;
        private IRowSelectionBuilder _rowSelectionBuilder = default!;

        IModelBuilder<TViewModel> IModelConfiguration<TViewModel>.Configure(IModelBuilder<TViewModel> modelBuilder, IElementSelectorBuilder elementSelectorBuilder, IRowSelectionBuilder rowSelectionBuilder)
        {
            _elementSelectorBuilder = elementSelectorBuilder;
            _rowSelectionBuilder = rowSelectionBuilder;

            return Configure(modelBuilder);
        }

        protected ElementSelector ById(string id) => _elementSelectorBuilder.ById(id);
        protected IFromTemplateSelectorBuilder FromTemplate(string templateId) => _elementSelectorBuilder.FromTemplate(templateId);
        protected ElementSelector ByQuerySelector(string querySelector) => _elementSelectorBuilder.ByQuerySelector(querySelector);
        protected RowSelector RowFromTemplate(string templateId) => _rowSelectionBuilder.FromTemplate(templateId);
        public abstract IModelBuilder<TViewModel> Configure(IModelBuilder<TViewModel> modelBuilder);
    }
}
