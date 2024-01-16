using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage;

internal interface IStoreElementsJsCodeBuilder
{
    string Build(IEnumerable<ElementObjectName> elements, string parentObjectName);
}
