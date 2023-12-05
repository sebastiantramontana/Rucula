using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Elements;

internal class CollectionTablePlaceBuilder<TViewModel> :
    ElementPlaceBuilderBase<IFinalizableCollectionTableBuilder<TViewModel>>
{
    public CollectionTablePlaceBuilder(TargetElement targetElement)
        : base(targetElement)
    {
    }

    protected override IFinalizableCollectionTableBuilder<TViewModel> CreateFinalizable(ValueModel parent)
        => new FinalizableCollectionTableBuilder<TViewModel>(parent);
}

