namespace WasmViewUpdater.Modeling.Building;

public interface IFinalizableModelBuilder<TViewModel, TFinalizable> : IModelBuilderData
{
    IElementBuilder<TFinalizable> Value<TReturn>(Func<TViewModel, TReturn> func);
    ITableBuilder<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func);
}
