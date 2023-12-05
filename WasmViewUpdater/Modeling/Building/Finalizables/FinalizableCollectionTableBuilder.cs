using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class FinalizableCollectionTableBuilder<TViewModel> :
    FinalizableCollectionBuilderBase<TViewModel>,
    IFinalizableCollectionTableBuilder<TViewModel>
{
    private readonly IElementBuilder<IFinalizableCollectionTableBuilder<TViewModel>> _elementBuilder;

    public FinalizableCollectionTableBuilder(ValueModel valueModel)
    {
        _elementBuilder = CreateElementBuilder(valueModel);
    }

    public IELementContentBuilder<IFinalizableCollectionTableBuilder<TViewModel>> ToContainerElement(ElementSelector selector)
        => _elementBuilder.ToContainerElement(selector);

    public IElementAttributeBuilder<IFinalizableCollectionTableBuilder<TViewModel>> ToElement(ElementSelector selector)
        => _elementBuilder.ToElement(selector);
}

