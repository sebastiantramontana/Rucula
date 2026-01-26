using Rucula.Presentation.ViewModels.Parameters;
using Vitraux;
using Vitraux.Modeling.Building.Contracts.ElementBuilders.Values.Root;

namespace Rucula.Presentation.Mappings;

internal static class ParameterValuesMappingExt
{
    private const string MinAttribute = "min";
    private const string MaxAttribute = "max";
    private const string ValueAttribute = "value";

    public static IRootValueFinallizable<TViewModel, TValue> MapParameterValue<TViewModel, TValue>(this IModelMapper<TViewModel> modelMapper, Func<TViewModel, TValue> valueFunc, string inputId)
        => modelMapper.MapValue(valueFunc).ToElements.ById(inputId).ToAttribute(ValueAttribute);

    public static IRootValueFinallizable<TViewModel, TValue> MapParametersRange<TViewModel, TValue>(this IRootValueFinallizable<TViewModel, TValue> finallizableMapper, IEnumerable<string> elementIds)
        where TViewModel : ParameterValuesViewModelBase
        => elementIds.Any()
            ? finallizableMapper
                .MapToRangeElementsToLimitAttribute(vm => vm.Min, elementIds, MinAttribute)
                .MapToRangeElementsToLimitAttribute(vm => vm.Max, elementIds, MaxAttribute)
            : finallizableMapper;

    private static IRootValueFinallizable<TViewModel, TValue> MapToRangeElementsToLimitAttribute<TViewModel, TValue>(this IRootValueFinallizable<TViewModel, TValue> finallizableMapper, Func<TViewModel, double> valueFunc, IEnumerable<string> elementIds, string limitAttribute) where TViewModel : ParameterValuesViewModelBase
    {
        if (!(elementIds?.Any() ?? false))
            return finallizableMapper;

        var mapValue = finallizableMapper.MapValue(valueFunc) as IRootValueMultiTargetBuilder<TViewModel, TValue>;

        foreach (var id in elementIds)
            mapValue = mapValue!.ToElements.ById(id).ToAttribute(limitAttribute);

        return (mapValue as IRootValueFinallizable<TViewModel, TValue>)!;
    }
}
