using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal interface IStorageElementJsLineGeneratorByTemplate
{
    string Generate(ElementObjectName elementObjectName);
}
