using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;

internal class QueryElementsOneTimeOnDemandJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOneTimeOnDemandJsCodeGenerator generator,
    IElementNamesGenerator elementNamesGenerator)
    : IQueryElementsOneTimeOnDemandJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementSelector> selectors, string parentObjectName)
        => builder.BuildJsCode(generator, elementNamesGenerator.Generate(selectors), parentObjectName);
}


