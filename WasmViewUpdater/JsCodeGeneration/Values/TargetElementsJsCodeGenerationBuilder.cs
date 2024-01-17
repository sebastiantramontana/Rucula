using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal class TargetElementsJsCodeGenerationBuilder(ITargetElementJsCodeGenerator targetElementJsCodeGeneration) : ITargetElementsJsCodeGenerationBuilder
{
    public string Build(ValueObjectName value, IEnumerable<ElementObjectName> elements)
        => value.AssociatedValue.TargetElements
            .Aggregate(new StringBuilder(), (sb, te) =>
            {
                var associatedElements = elements.Where(e => e.AssociatedSelector == te.Selector);
                return sb.Append(targetElementJsCodeGeneration.GenerateJsCode(te, associatedElements, value.Name));
            })
            .ToString();
}
