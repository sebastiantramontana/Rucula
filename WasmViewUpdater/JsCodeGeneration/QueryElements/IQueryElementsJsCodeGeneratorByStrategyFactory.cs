using Vitraux.Modeling.Building;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal interface IQueryElementsJsCodeGeneratorByStrategyFactory
{
    IQueryElementsJsCodeGenerator GetInstance(QueryElementStrategy strategy);
}
