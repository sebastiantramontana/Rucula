using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Elements;

internal class ToFinalizableValueElementModel : ToFinalizableElementModelBase<IFinalizableValueModel>
{
    public ToFinalizableValueElementModel(TargetElement targetElement) : base(targetElement)
    {
    }

    protected override IFinalizableValueModel CreateFinalizable(ValueModel parent)
        => new FinalizableValueModel(parent);
}

