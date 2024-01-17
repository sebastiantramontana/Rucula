namespace Vitraux.JsCodeGeneration.Values;

internal class ElementPlaceContentJsCodeGenerator : IElementPlaceContentJsCodeGenerator
{
    public string Generate(string elementObjectName, string valueObjectName)
        => $"globalThis.vitraux.updating.setElementsContent({elementObjectName}, vm.{valueObjectName});";
}
