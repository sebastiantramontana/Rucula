using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling
{
    public abstract class ModelConfigurationBase<TViewModel> : IModelConfiguration<TViewModel>
    {
        private IElementSelectorFactory _elementSelectorFactory = default!;
        private IRowSelectionFactory _rowSelectionFactory = default!;

        IModelBuilder<TViewModel> IModelConfiguration<TViewModel>.Configure(IModelBuilder<TViewModel> modelBuilder, IElementSelectorFactory elementSelectorFactory, IRowSelectionFactory rowSelectionFactory)
        {
            _elementSelectorFactory = elementSelectorFactory;
            _rowSelectionFactory = rowSelectionFactory;

            return Configure(modelBuilder);
        }

        protected ElementSelector ById(string id) => _elementSelectorFactory.ById(id);
        protected IFromTemplateSelector FromTemplate(string templateId) => _elementSelectorFactory.FromTemplate(templateId);
        protected ElementSelector ByQuerySelector(string querySelector) => _elementSelectorFactory.ByQuerySelector(querySelector);
        protected IRowSelection RowFromTemplate(string templateId) => _rowSelectionFactory.FromTemplate(templateId);
        public abstract IModelBuilder<TViewModel> Configure(IModelBuilder<TViewModel> modelBuilder);
    }
}
