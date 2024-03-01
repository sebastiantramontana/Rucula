using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit;

internal class QueryElementsDeclaringOneTimeOnInitJsCodeGenerator : IQueryElementsDeclaringOneTimeOnInitJsCodeGenerator
{
    public string GenerateJsCode(string parentObjectName, ElementObjectName elementObjectName)
    {
        var elementCodeBuilder = new StringBuilder();
        elementCodeBuilder.Append($"const {elementObjectName.Name} = globalThis.vitraux.storedElements.elements.{parentObjectName}.{elementObjectName.Name};");

        if (elementObjectName is ElementTemplateObjectName templateObjectName)
        {
            elementCodeBuilder.AppendLine();
            elementCodeBuilder.Append($"const {templateObjectName.AppendToName} = globalThis.vitraux.storedElements.elements.{parentObjectName}.{templateObjectName.AppendToName};");
        }

        return elementCodeBuilder.ToString();
    }
}
