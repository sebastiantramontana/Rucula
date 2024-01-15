using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysCodeGenerator(IJsQueryElementsDeclaringAlwaysGeneratorFactory factory) : IQueryElementsDeclaringAlwaysCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector elementSelector)
        => factory
            .GetInstance(elementSelector)
            .GenerateJsCode(elementObjectName, parentObjectName, elementSelector);
}
