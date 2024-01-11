using System.Text;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryElementsJsCodeBuilder : IQueryElementsJsCodeBuilder
{
    public string BuildJsCode(IQueryElementsDeclaringJsCodeGenerator declaringJsCodeGenerator, IEnumerable<ElementSelector> selectors, string parentObjectName)
    {
        const string ElementNamePrefix = "element";
        var numberPosfix = 1;

        var stringBuilder = new StringBuilder();

        foreach (var selector in selectors)
        {
            var elementName = ElementNamePrefix + numberPosfix;
            var code = declaringJsCodeGenerator.GenerateJsCode(elementName, parentObjectName, selector);

            stringBuilder.AppendLine(code);
            numberPosfix++;
        }

        return stringBuilder.ToString();
    }
}


