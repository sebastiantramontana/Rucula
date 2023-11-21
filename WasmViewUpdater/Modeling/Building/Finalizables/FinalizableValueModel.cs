using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Finalizables;

internal class FinalizableValueModel : BuildingValueModel, IFinalizableValueModel
{
    public FinalizableValueModel(ValueModel valueModel)
        : base(valueModel)
    {
    }
}

