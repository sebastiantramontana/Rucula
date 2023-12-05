using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal abstract class FinalizableCollectionBuilderBase<TViewModel> : ModelBuilderBase<TViewModel, IFinalizableCollectionTableBuilder<TViewModel>>
{
    protected sealed override IElementBuilder<IFinalizableCollectionTableBuilder<TViewModel>> CreateElementBuilder(ValueModel valueModel)
        => new CollectionTableElementBuilder<TViewModel>(valueModel);
}

