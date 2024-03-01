using System.Text;
using Vitraux.JsCodeGeneration.QueryElements;
using Vitraux.JsCodeGeneration.QueryElements.ElementsGeneration;
using Vitraux.JsCodeGeneration.Values;
using Vitraux.Modeling.Building;
using Vitraux.Modeling.Building.Selectors.Elements;

namespace Vitraux.JsCodeGeneration;

internal class JsGenerator<TViewModel>(
    IQueryElementsJsCodeGeneratorByStrategyFactory queryElementsJsCodeGeneratorFactory,
    IElementNamesGenerator elementNamesGenerator,
    IValueNamesGenerator valueNamesGenerator,
    IValuesJsCodeGenerator valueJsCodeGenerator)
    : IJsGenerator<TViewModel>
{
    public string GenerateJsCode(IModelBuilderData modelBuilderData)
    {
        var rootObject = "document";
        var selectors = GroupSelectors(modelBuilderData);
        var elements = elementNamesGenerator.Generate(selectors);
        var valueNames = valueNamesGenerator.Generate(modelBuilderData.Values);

        return new StringBuilder()
            .AppendLine(GenerateQueryElementsJsCode(modelBuilderData.QueryElementStrategy, elements, rootObject))
            .AppendLine()
            .AppendLine(GenerateValuesJsCode(valueNames, elements))
            .ToString()
            .Trim();
    }

    private string GenerateValuesJsCode(IEnumerable<ValueObjectName> valueNames, IEnumerable<ElementObjectName> elements)
        => valueJsCodeGenerator
            .GenerateJsCode(valueNames, elements)
            .Trim();

    private string GenerateQueryElementsJsCode(QueryElementStrategy strategy, IEnumerable<ElementObjectName> elements, string rootObject)
        => queryElementsJsCodeGeneratorFactory
                .GetInstance(strategy)
                .GenerateJsCode(elements, rootObject)
                .Trim();

    private IEnumerable<ElementSelector> GroupSelectors(IModelBuilderData modelBuilderData)
        => modelBuilderData
            .Values
            .SelectMany(v => v.TargetElements.Select(te => te.Selector))
            .Distinct()
            .Concat(modelBuilderData.CollectionTables.Select(c => c.TableSelector));
}
