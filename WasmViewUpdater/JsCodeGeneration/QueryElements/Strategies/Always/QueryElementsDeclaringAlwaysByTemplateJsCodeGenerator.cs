using Vitraux.JsCodeGeneration.BuiltInCalling.StoredElements;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;

internal class QueryElementsDeclaringAlwaysByTemplateJsCodeGenerator(IGetElementByTemplateAsArrayCall getElementByTemplateAsArrayCalling) : IQueryElementsDeclaringAlwaysByTemplateJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
        => $"const {elementObjectName} = {getElementByTemplateAsArrayCalling.Generate(selector.Value)};";
}
