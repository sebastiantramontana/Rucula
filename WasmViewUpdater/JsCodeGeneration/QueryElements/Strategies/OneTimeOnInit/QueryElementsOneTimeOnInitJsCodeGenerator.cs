using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit;

internal class QueryElementsOneTimeOnInitJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOneTimeOnInitJsCodeGenerator declaringGenerator,
    IQueryElementsOneTimeOnInitInitializer initializer
    ) : IQueryElementsOneTimeOnInitJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementObjectName> elements, string parentObjectName)
    {
        initializer.StoreElementsInAdvance(elements, parentObjectName);

        return builder.BuildJsCode(declaringGenerator, elements, parentObjectName);
    }
}


