using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration;

internal class JsGenerator<TViewModel>(IQueryElementsJsCodeGeneratorByStrategyFactory queryElementsJsCodeGeneratorFactory)
    : IJsGenerator<TViewModel>
{
    public string GenerateJsCode(IModelBuilderData modelBuilderData)
    {
        var rootObject = "document";
        var selectors = GroupSelectors(modelBuilderData);

        return queryElementsJsCodeGeneratorFactory
                .GetInstance(modelBuilderData.QueryElementStrategy)
                .GenerateJsCode(selectors, rootObject)
                .Trim();
    }

    private IEnumerable<ElementSelector> GroupSelectors(IModelBuilderData modelBuilderData)
        => modelBuilderData
            .Values
            .SelectMany(v => v.TargetElements)
            .GroupBy(t => t.Selector)
            .Select(g => g.Key)
            .Union(modelBuilderData.CollectionTables.Select(c => c.TableSelector));
}
