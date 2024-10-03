using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Helpers;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToDolarWesternUnionDtoDeserializer : IJsonDeserializer<DolarWesternUnionDto>
{
    private const int acFundInIndex = 0;
    private const string fxRateNodeProperty = "fx_rate";
    private const string grossFeeNodeProperty = "gross_fee";
    private const string payGroupNodeKey = "pay_groups";

    private readonly IJsonValueReader _valueReader;

    public JsonToDolarWesternUnionDtoDeserializer(IJsonValueReader valueReader)
        => _valueReader = valueReader;

    public Optional<DolarWesternUnionDto> Deserialize(JsonNode? node)
        => Optional<DolarWesternUnionDto>.Maybe(DeserializeIncludingFees(node));

    private DolarWesternUnionDto? DeserializeIncludingFees(JsonNode? node) 
        => InvocationChainNullable<JsonNode>
            .Create(node)
            .IfNotNull(node => GetServiceNode(node, "500") ?? GetServiceNode(node, "800"))
            .IfNotNull(serviceNode => serviceNode?[payGroupNodeKey]?[acFundInIndex])
            .IfNotNull(acPayGroupNode => GetNodeValue<double>(acPayGroupNode, fxRateNodeProperty))
            .IfNotEmpty((double fxRate, JsonNode? acPayGroupNode) => GetNodeValue<double>(acPayGroupNode, grossFeeNodeProperty))
            .Return((double grossFee, double fxRate) => new DolarWesternUnionDto(fxRate, grossFee));

    private JsonNode? GetServiceNode(JsonNode node, string serviceNumber)
    {
        var array = node["services_groups"] as JsonArray;
        var serviceNode = array?.SingleOrDefault(n => GetNodeValue<string>(n, "service").Equals(serviceNumber));

        return serviceNode;
    }

    private Optional<T> GetNodeValue<T>(JsonNode? node, string property)
        => _valueReader.GetValue<T>(node, property);
}
