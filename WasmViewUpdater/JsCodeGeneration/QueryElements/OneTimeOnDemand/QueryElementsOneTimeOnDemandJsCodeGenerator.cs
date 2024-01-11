using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.OneTimeOnDemand;

internal class QueryElementsOneTimeOnDemandJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOneTimeOnDemandJsCodeGenerator generator)
    : IQueryElementsOneTimeOnDemandJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementSelector> selectors, string parentObjectName)
        => builder.BuildJsCode(generator, selectors, parentObjectName);
}


