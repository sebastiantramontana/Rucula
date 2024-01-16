using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal interface IStorageElementJsLineGeneratorByQuerySelector
{
    string Generate(ElementObjectName elementObjectName, string parentObjectName);
}
