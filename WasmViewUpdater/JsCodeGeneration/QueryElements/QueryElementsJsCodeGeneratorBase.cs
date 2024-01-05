using System.Text;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal abstract class QueryElementsJsCodeGeneratorBase : IQueryElementsJsCodeGenerator
{
    public string GenerateJsCode(IEnumerable<ElementSelector> selectors, string parentObjectName)
    {
        const string ElementNamePrefix = "element";
        var numberPosfix = 1;

        var stringBuilder = new StringBuilder();

        foreach (var selector in selectors)
        {
            var elementName = ElementNamePrefix + numberPosfix;
            var code = GenerateJsCodeLine(elementName, parentObjectName, selector);

            stringBuilder.AppendLine(code);
            numberPosfix++;
        }

        return stringBuilder.ToString();
    }

    protected abstract string GenerateJsCodeLine(string elementName, string parentObjectName, ElementSelector selector);
}


