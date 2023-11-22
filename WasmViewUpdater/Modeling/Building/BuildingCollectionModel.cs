using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

internal class BuildingCollectionModel<TViewModel> : IBuildingCollectionModel<TViewModel>
{
    private readonly CollectionTableModel _collectionTableModel;

    public BuildingCollectionModel(CollectionTableModel collectionTableModel)
    {
        _collectionTableModel = collectionTableModel;
    }

    public IToTableModel<TViewModel> ToTable(ElementSelector selector)
    {
        _collectionTableModel.TableSelector = selector;
        return new ToTableModel<TViewModel>(_collectionTableModel);
    }
}

