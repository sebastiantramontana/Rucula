using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Models;

namespace Vitraux.Modeling.Building;

public interface IModelBuilderData
{
    internal IEnumerable<ValueModel> Values { get; }
    internal IEnumerable<CollectionTableModel> CollectionTables { get; }
    QueryElementStrategy QueryElementStrategy { get; set; }
    bool TrackChanges { get; set; }
}
