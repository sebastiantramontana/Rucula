using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage;

internal interface IQueryElementsOneTimeOnInitInitializer
{
    void StoreElementsInAdvance(IEnumerable<ElementObjectName> elements, string parentObjectName);
}
