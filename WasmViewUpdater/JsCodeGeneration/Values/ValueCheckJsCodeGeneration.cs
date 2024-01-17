using System.Text;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.Values;

internal class ValueCheckJsCodeGeneration(ITargetElementsJsCodeGenerationBuilder targetElementJsBuilder) : IValueCheckJsCodeGeneration
{
    public string GenerateJsCode(ValueObjectName value, IEnumerable<ElementObjectName> elements)
        => new StringBuilder()
            .AppendLine($"if(vm.{value.Name}) {{")
            .Append(targetElementJsBuilder.Build(value, elements))
            .AppendLine("}")
            .ToString();
}
