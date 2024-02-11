using System.Text;
using Vitraux.Modeling.Building.Selectors.Elements;
using Vitraux.Modeling.Building.Selectors.Elements.Templates;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit;

internal class QueryElementsDeclaringOneTimeOnInitJsCodeGenerator : IQueryElementsDeclaringOneTimeOnInitJsCodeGenerator
{
    public string GenerateJsCode(string elementObjectName, string parentObjectName, ElementSelector selector)
    {
        var elementCodeBuilder = new StringBuilder($"const {elementObjectName} = globalThis.vitraux.storedElements.elements.{parentObjectName}.{elementObjectName};");

        if (selector is ElementTemplateSelector)
        {
            elementCodeBuilder
                .AppendLine()
                .Append($"const {elementObjectName}_appendTo = globalThis.vitraux.storedElements.elements.{parentObjectName}.{elementObjectName}_appendTo;");
        }

        return elementCodeBuilder.ToString();
    }
}
