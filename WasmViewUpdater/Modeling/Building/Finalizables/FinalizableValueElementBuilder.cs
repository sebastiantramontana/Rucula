using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Finalizables;

internal class FinalizableValueElementBuilder : ValueElementBuilder, IFinalizableElementBuilder
{
    public FinalizableValueElementBuilder(ValueModel valueModel)
        : base(valueModel)
    {
    }
}

