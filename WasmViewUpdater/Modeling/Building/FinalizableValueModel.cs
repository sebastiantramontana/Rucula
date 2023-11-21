using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class FinalizableValueModel : BuildingValueModel, IFinalizableValueModel
{
    public FinalizableValueModel(ValueModel valueModel)
        : base(valueModel)
    {
    }
}

