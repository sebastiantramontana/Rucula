using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building;

internal class ModelBuilder<TViewModel, TSelector> : IModelBuilder<TViewModel, TSelector> where TSelector : ElementSelector
{
    private readonly ICollection<ValueModel> _values;
    private readonly ICollection<CollectionTableModel> _collections;

    internal ModelBuilder()
    {
        _values = [];
        _collections = [];
    }

    IEnumerable<ValueModel> IModelBuilderData.Values => _values;
    IEnumerable<CollectionTableModel> IModelBuilderData.CollectionTables => _collections;

    public QueryElementStrategy QueryElementStrategy { get; set; } = QueryElementStrategy.OneTimeOnInit;
    public bool TrackChanges { get; set; } = false;

    public IElementBuilder<TViewModel, TSelector> Value<TReturn>(Func<TViewModel, TReturn> func)
    {
        var valueModel = new ValueModel(func);
        _values.Add(valueModel);

        return new ValueModelBuilder<TViewModel, TSelector>(valueModel, this);
    }

    public ITableBuilder<TReturn> Collection<TReturn>(Func<TViewModel, IEnumerable<TReturn>> func)
    {
        var collectionTableModel = new CollectionTableModel(func);
        _collections.Add(collectionTableModel);

        return new CollectionTableModelBuilder<TReturn>(collectionTableModel);
    }
}
