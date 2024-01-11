using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Always;

internal class QueryElementsAlwaysJsCodeGenerator(
    IQueryElementsDeclaringAlwaysCodeGenerator generator,
    IQueryElementsJsCodeBuilder builder)
    : IQueryElementsAlwaysJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementSelector> selectors, string parentObjectName)
        => builder.BuildJsCode(generator, selectors, parentObjectName);
}


