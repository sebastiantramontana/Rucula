using Rucula.DataAccess.Dtos;
using Rucula.DataAccess.Helpers;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToDolarWesternUnionDtoDeserializer : IJsonDeserializer<DolarWesternUnionDto>
{
    private const int acFundInIndex = 0;
    private readonly IJsonValueReader _valueReader;

    public JsonToDolarWesternUnionDtoDeserializer(IJsonValueReader valueReader)
        => _valueReader = valueReader;

    public Optional<DolarWesternUnionDto> Deserialize(JsonNode? node)
        => Optional<DolarWesternUnionDto>.Maybe(DeserializeIncludingFees(node));

    private DolarWesternUnionDto? DeserializeIncludingFees(JsonNode? node)
    {
        return InvocationChainNullable<JsonNode>
            .Create(node)
            .IfNotNull(node => GetServiceNode(node, "500"))
            .IfNotNull(service500node => service500node?["pay_groups"]?[acFundInIndex])
            .IfNotNull(acPayGroupNode => GetNodeValue<double>(acPayGroupNode, "strike_fx_rate"))
            .IfNotEmpty((double strikeFxRate, JsonNode? acPayGroupNode) => GetNodeValue<double>(acPayGroupNode, "gross_fee"))
            .Return((double grossFee, double strikeFxRate) => new DolarWesternUnionDto(strikeFxRate, grossFee));
    }

    private JsonNode? GetServiceNode(JsonNode node, string serviceNumber)
    {
        var array = node["services_groups"] as JsonArray;
        var serviceNode = array?.SingleOrDefault(n => GetNodeValue<string>(n, "service").Equals(serviceNumber));

        return serviceNode;
    }

    private Optional<T> GetNodeValue<T>(JsonNode? node, string property)
        => _valueReader.GetValue<T>(node, property);
}
