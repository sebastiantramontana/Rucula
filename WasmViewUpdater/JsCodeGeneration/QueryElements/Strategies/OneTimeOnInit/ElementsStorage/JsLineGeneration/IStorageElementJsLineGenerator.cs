using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;

namespace Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit.ElementsStorage.JsLineGeneration;

internal interface IStorageElementJsLineGenerator
{
    string Generate(ElementObjectName elementObjectName, string parentObjectName);
}
