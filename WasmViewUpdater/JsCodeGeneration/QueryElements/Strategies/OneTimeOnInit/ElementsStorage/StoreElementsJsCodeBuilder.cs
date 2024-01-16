using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage;

internal class StoreElementsJsCodeBuilder(IStorageElementJsLineGenerator lineGenerator) : IStoreElementsJsCodeBuilder
{
    public string Build(IEnumerable<ElementObjectName> elements, string parentObjectName)
        => elements
            .Aggregate(new StringBuilder(), (sb, element) => sb.AppendLine(lineGenerator.Generate(element, parentObjectName)))
            .ToString()
            .Trim();
}
