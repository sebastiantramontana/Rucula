using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage;

internal class QueryElementsOneTimeOnInitInitializer(IStoreElementsJsCodeBuilder codeBuilder, IJsCodeExecutor codeExecutor) : IQueryElementsOneTimeOnInitInitializer
{
    public void StoreElementsInAdvance(IEnumerable<ElementObjectName> elements, string parentObjectName)
        => codeExecutor.Excute(codeBuilder.Build(elements, parentObjectName));
}