using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsOneTimeOnDemandJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOneTimeOnDemandJsCodeGenerator generator)
    : IQueryElementsOneTimeOnDemandJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementObjectName> elements, string parentObjectName)
        => builder.BuildJsCode(generator, elements, parentObjectName);
}


