using System;
using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Finalizables;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;
using WasmViewUpdater.Modeling.Models;

namespace WasmViewUpdater.Modeling.Building;

public interface IModelBuilder<TEntity> : IFinalizableModelBuilder<TEntity, IFinalizableValueModel>
{
}

internal class ModelBuilder<TEntity> : IModelBuilder<TEntity>
{
    private readonly ICollection<ValueModel> _values;
    private readonly ICollection<CollectionTableModel> _collections;

    internal ModelBuilder()
    {
        _values = new List<ValueModel>();
        _collections = new List<CollectionTableModel>();
    }

    IEnumerable<ValueModel> IFinalizableModelBuilder<TEntity, IFinalizableValueModel>.Values => _values;
    IEnumerable<CollectionTableModel> IFinalizableModelBuilder<TEntity, IFinalizableValueModel>.CollectionTables => _collections;

    public IBuildingCollectionModel<TReturn> Collection<TReturn>(Func<TEntity, IEnumerable<TReturn>> func)
    {
        _collections.Add(new CollectionTableModel(func));

        return default!;
    }

    public IBuildingValueModel<IFinalizableValueModel> Value<TReturn>(Func<TEntity, TReturn> func)
    {
        var newValueModel = new ValueModel(func);
        _values.Add(newValueModel);

        return new BuildingValueModel(newValueModel);
    }

    internal class BuildingValueModel : IBuildingValueModel<IFinalizableValueModel>
    {
        private readonly ValueModel _valueModel;

        public BuildingValueModel(ValueModel valueModel)
        {
            _valueModel = valueModel;
        }

        public IToContainerElementModel<IFinalizableValueModel> ToContainerElement(ElementSelector selector)
            => new ToFinalizableValueElementModel(AddNewTargetElement(selector));

        public IToElementModel<IFinalizableValueModel> ToElement(ElementSelector selector)
            => new ToFinalizableValueElementModel(AddNewTargetElement(selector));

        private TargetElement AddNewTargetElement(ElementSelector selector)
        {
            var newTargetElement = new TargetElement(selector, _valueModel);
            _valueModel.TargetElements = _valueModel.TargetElements.Append(newTargetElement);

            return newTargetElement;
        }
    }

    internal class ToFinalizableValueElementModel
        : IToContainerElementModel<IFinalizableValueModel>, IToElementModel<IFinalizableValueModel>
    {
        private readonly TargetElement _targetElement;

        public ToFinalizableValueElementModel(TargetElement targetElement)
        {
            _targetElement = targetElement;
        }

        public IFinalizableValueModel ToAttribute(string attribute)
        {
            _targetElement.Place = new ElementPlace(ElementPlacing.Attribute, attribute);
            return new FinalizableValueModel(_targetElement.Parent);
        }

        public IFinalizableValueModel ToContent()
        {
            _targetElement.Place = new ElementPlace(ElementPlacing.Content);
            return new FinalizableValueModel(_targetElement.Parent);
        }
    }

    internal class FinalizableValueModel : BuildingValueModel, IFinalizableValueModel
    {
        public FinalizableValueModel(ValueModel valueModel)
            : base(valueModel)
        {
        }
    }
}
