using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class CollectionTableElementBuilder<TViewModel> :
    ElementBuilderBase<IFinalizableCollectionTableBuilder<TViewModel>>,
    IElementBuilder<IFinalizableCollectionTableBuilder<TViewModel>>
{
    public CollectionTableElementBuilder(ValueModel valueModel)
        : base(valueModel)
    {
    }

    protected override IELementContentBuilder<IFinalizableCollectionTableBuilder<TViewModel>> CreateELementContentBuilder(TargetElement targetElement)
        => new CollectionTablePlaceBuilder<TViewModel>(targetElement);

    protected override IElementAttributeBuilder<IFinalizableCollectionTableBuilder<TViewModel>> CreateElementAttributeBuilder(TargetElement targetElement)
        => new CollectionTablePlaceBuilder<TViewModel>(targetElement);
}

