using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements.Builders;
using Vitraux.Modeling.Building.Selectors.TableRows;

namespace Vitraux.Modeling
{
    public interface IModelConfiguration<TViewModel>
    {
        internal void Configure(IModelBuilder<TViewModel> modelBuilder,
                                IElementSelectorBuilder elementSelectorBuilder,
                                IRowSelectorBuilder rowSelectionBuilder,
                                ITemplateChildElementSelectorBuilder templateChildElementSelectorBuilder);
    }
}
