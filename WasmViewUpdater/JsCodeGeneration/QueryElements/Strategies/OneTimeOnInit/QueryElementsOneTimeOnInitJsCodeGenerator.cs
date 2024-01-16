using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit;

internal class QueryElementsOneTimeOnInitJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOneTimeOnInitJsCodeGenerator declaringGenerator,
    IElementNamesGenerator elementNamesGenerator,
    IQueryElementsOneTimeOnInitInitializer initializer
    ) : IQueryElementsOneTimeOnInitJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementSelector> selectors, string parentObjectName)
    {
        var elementObajectNames = elementNamesGenerator.Generate(selectors);

        initializer.StoreElementsInAdvance(elementObajectNames, parentObjectName);

        return builder.BuildJsCode(declaringGenerator, elementObajectNames, parentObjectName);
    }
}


