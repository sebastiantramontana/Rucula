namespace Vitraux.JsCodeGeneration.Values;

internal class ElementPlaceAttributeJsCodeGenerator : IElementPlaceAttributeJsCodeGenerator
{
    public string Generate(string attribute, string elementObjectName, string valueObjectName)
        => $"globalThis.vitraux.updating.setElementsAttribute({elementObjectName}, '{attribute}', vm.{valueObjectName});";
}
