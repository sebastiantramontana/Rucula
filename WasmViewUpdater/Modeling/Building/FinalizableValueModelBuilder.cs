using WasmViewUpdater.Modeling.Building;
using WasmViewUpdater.Modeling.Building.Elements;
using WasmViewUpdater.Modeling.Building.Selectors.Elements;

namespace WasmViewUpdater.Modeling.Models;

internal class FinalizableValueModelBuilder<TViewModel> : IFinalizableBuilder<TViewModel>
{
    private readonly ICollection<ValueModel> _values;
    private readonly ICollection<CollectionTableModel> _collections;
    private readonly IModelBuilder<TViewModel> _innerModelBuilder;
    private readonly IElementBuilder<TViewModel> _innerElementBuilder;

    public FinalizableValueModelBuilder(IModelBuilder<TViewModel> innerModelBuilder, IElementBuilder<TViewModel> innerElementBuilder)
    {
        _values = innerModelBuilder.Values.ToList();
        _collections = innerModelBuilder.CollectionTables.ToList();
        _innerModelBuilder = innerModelBuilder;
        _innerElementBuilder = innerElementBuilder;
    }

    IEnumerable<ValueModel> IModelBuilderData.Values => _values;
    IEnumerable<CollectionTableModel> IModelBuilderData.CollectionTables => _collections;

    public IElementBuilder<TViewModel> Value<TReturn>(Func<TViewModel, TReturn> func)
        => _innerModelBuilder.Value(func);

    //TODO: revisar
    public ITableBuilder<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
        => _innerModelBuilder.Collection(func);

    public IELementContentBuilder<TViewModel> ToContainerElement(ElementSelector selector)
        => _innerElementBuilder.ToContainerElement(selector);

    public IElementAttributeBuilder<TViewModel> ToElement(ElementSelector selector)
        => _innerElementBuilder.ToElement(selector);
}
