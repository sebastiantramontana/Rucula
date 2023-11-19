using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.TableRows;

namespace WasmViewUpdater.Modeling
{
    public abstract class ModelConfigurationBase<TEntity> : IModelConfiguration<TEntity>
    {
        private IElementSelectorFactory _elementSelectorFactory = default!;
        private IRowSelectionFactory _rowSelectionFactory = default!;

        IModelBuilder<TEntity> IModelConfiguration<TEntity>.Configure(IModelBuilder<TEntity> modelBuilder, IElementSelectorFactory elementSelectorFactory, IRowSelectionFactory rowSelectionFactory)
        {
            _elementSelectorFactory = elementSelectorFactory;
            _rowSelectionFactory = rowSelectionFactory;

            return Configure(modelBuilder);
        }

        protected IElementSelector ById(string id) => _elementSelectorFactory.ById(id);
        protected IFromTemplateSelector FromTemplate(string templateId) => _elementSelectorFactory.FromTemplate(templateId);
        protected IElementSelector ByQuerySelector(string querySelector) => _elementSelectorFactory.ByQuerySelector(querySelector);
        protected IRowSelection RowFromTemplate(string templateId) => _rowSelectionFactory.FromTemplate(templateId);
        public abstract IModelBuilder<TEntity> Configure(IModelBuilder<TEntity> modelBuilder);
    }
}
