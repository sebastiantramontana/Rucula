namespace WasmViewUpdater.Modeling.Building;

public interface IFinalizableModelBuilder<TViewModel, TFinalizable> : IModelBuilderData
{
    IBuildingValueModel<TFinalizable> Value<TReturn>(Func<TViewModel, TReturn> func);
    IBuildingCollectionModel<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func);
}
