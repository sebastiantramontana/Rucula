using Vitraux.Modeling.Building.Elements;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building;

internal class FinalizableValueModelBuilder<TViewModel, TSelector> : IFinalizableBuilder<TViewModel, TSelector> where TSelector : ElementSelector
{
    private readonly ICollection<ValueModel> _values;
    private readonly ICollection<CollectionTableModel> _collections;
    private readonly IModelBuilder<TViewModel, TSelector> _innerModelBuilder;
    private readonly IElementBuilder<TViewModel, TSelector> _innerElementBuilder;

    internal FinalizableValueModelBuilder(IModelBuilder<TViewModel, TSelector> innerModelBuilder, IElementBuilder<TViewModel, TSelector> innerElementBuilder)
    {
        _values = innerModelBuilder.Values.ToList();
        _collections = innerModelBuilder.CollectionTables.ToList();
        _innerModelBuilder = innerModelBuilder;
        _innerElementBuilder = innerElementBuilder;
    }

    IEnumerable<ValueModel> IModelBuilderData.Values => _values;
    IEnumerable<CollectionTableModel> IModelBuilderData.CollectionTables => _collections;

    public QueryElementStrategy QueryElementStrategy
    {
        get => _innerModelBuilder.QueryElementStrategy;
        set => _innerModelBuilder.QueryElementStrategy = value;
    }

    public bool TrackChanges
    {
        get => _innerModelBuilder.TrackChanges;
        set => _innerModelBuilder.TrackChanges = value;
    }

    public IElementBuilder<TViewModel, TSelector> Value<TReturn>(Func<TViewModel, TReturn> func)
        => _innerModelBuilder.Value(func);

    public ITableBuilder<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
        => _innerModelBuilder.Collection(func);

    public IELementContentBuilder<TViewModel, TSelector> ToContainerElement(TSelector selector)
        => _innerElementBuilder.ToContainerElement(selector);

    public IElementAttributeBuilder<TViewModel, TSelector> ToElement(TSelector selector)
        => _innerElementBuilder.ToElement(selector);
}
