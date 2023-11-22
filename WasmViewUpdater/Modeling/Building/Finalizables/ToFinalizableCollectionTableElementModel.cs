using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building.Finalizables;

internal class ToFinalizableCollectionTableElementModel<TViewModel> :
    ToFinalizableElementModelBase<IFinalizableCollectionTableModel<TViewModel>>
{
    public ToFinalizableCollectionTableElementModel(TargetElement targetElement)
        : base(targetElement)
    {
    }

    protected override IFinalizableCollectionTableModel<TViewModel> CreateFinalizable(ValueModel parent)
        => new FinalizableCollectionTableModel<TViewModel>(parent);
}

