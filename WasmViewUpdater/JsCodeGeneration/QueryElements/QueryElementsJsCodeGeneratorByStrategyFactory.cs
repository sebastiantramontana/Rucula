using Vitraux.JsCodeGeneration.QueryElements.Strategies.Always;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnDemand;
using Vitraux.JsCodeGeneration.QueryElements.Strategies.OneTimeOnInit;
using Vitraux.Modeling.Building;

namespace Vitraux.JsCodeGeneration.QueryElements;

internal class QueryElementsJsCodeGeneratorByStrategyFactory(
    IQueryElementsOneTimeOnInitJsCodeGenerator onInitGenerator,
    IQueryElementsOneTimeOnDemandJsCodeGenerator onDemandGenerator,
    IQueryElementsAlwaysJsCodeGenerator alwaysGenerator)
    : IQueryElementsJsCodeGeneratorByStrategyFactory
{
    public IQueryElementsJsCodeGenerator GetInstance(QueryElementStrategy strategy)
    => strategy switch
    {
        QueryElementStrategy.OneTimeOnInit => onInitGenerator,
        QueryElementStrategy.OneTimeOnDemand => onDemandGenerator,
        QueryElementStrategy.Always => alwaysGenerator,
        _ => throw new NotImplementedException($"JS Code Generator not implemented for {strategy} case"),
    };
}
