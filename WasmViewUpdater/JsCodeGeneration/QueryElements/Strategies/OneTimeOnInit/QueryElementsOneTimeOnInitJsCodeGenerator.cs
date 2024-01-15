using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit;

internal class QueryElementsOneTimeOnInitJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOneTimeOnInitJsCodeGenerator declaringGenerator,
    IElementNamesGenerator elementNamesGenerator
    ) : IQueryElementsOneTimeOnInitJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementSelector> selectors, string parentObjectName)
    {
        var elementObajectNames = elementNamesGenerator.Generate(selectors);
        //TODO: INITIALIZE ELEMENTS

        return builder.BuildJsCode(declaringGenerator, elementObajectNames, parentObjectName);
    }
}


