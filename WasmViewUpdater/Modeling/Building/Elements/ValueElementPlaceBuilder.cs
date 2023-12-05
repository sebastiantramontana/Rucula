using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Elements;

internal class ValueElementPlaceBuilder : ElementPlaceBuilderBase<IFinalizableElementBuilder>
{
    public ValueElementPlaceBuilder(TargetElement targetElement) 
        : base(targetElement)
    {
    }

    protected override IFinalizableElementBuilder CreateFinalizable(ValueModel parent)
        => new FinalizableValueElementBuilder(parent);
}

