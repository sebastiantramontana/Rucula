using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class ValueElementBuilder : ElementBuilderBase<IFinalizableElementBuilder>
{
    public ValueElementBuilder(ValueModel valueModel)
        : base(valueModel)
    {
    }

    protected override IELementContentBuilder<IFinalizableElementBuilder> CreateELementContentBuilder(TargetElement targetElement)
        => new ValueElementPlaceBuilder(targetElement);

    protected override IElementAttributeBuilder<IFinalizableElementBuilder> CreateElementAttributeBuilder(TargetElement targetElement)
        => new ValueElementPlaceBuilder(targetElement);
}

