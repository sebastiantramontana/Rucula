using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.OneTimeOnInit;

internal class QueryElementsOneTimeOnInitJsCodeGenerator(
    IQueryElementsJsCodeBuilder builder,
    IQueryElementsDeclaringOneTimeOnInitJsCodeGenerator declaringGenerator
    ) : IQueryElementsOneTimeOnInitJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementSelector> selectors, string parentObjectName)
    {
        //TODO: INITIALIZE ELEMENTS

        return builder.BuildJsCode(declaringGenerator, selectors, parentObjectName);
    }
}


