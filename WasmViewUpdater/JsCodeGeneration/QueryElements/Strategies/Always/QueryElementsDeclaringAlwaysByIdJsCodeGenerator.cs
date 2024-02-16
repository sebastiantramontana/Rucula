using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByIdJsCodeGenerator(IGetElementByIdAsArrayCall getElementByIdAsArrayCalling) : IQueryElementsDeclaringAlwaysByIdJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = {getElementByIdAsArrayCalling.Generate(parentObjectName, selector.Value)};";
}
