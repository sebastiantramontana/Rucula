using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class ModelBuilder<TViewModel> : ModelBuilderBase<TViewModel, IFinalizableElementBuilder>, IModelBuilder<TViewModel>
{
    protected override IElementBuilder<IFinalizableElementBuilder> CreateElementBuilder(ValueModel valueModel)
        => new ValueElementBuilder(valueModel);
}
