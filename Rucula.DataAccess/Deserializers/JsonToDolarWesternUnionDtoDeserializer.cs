using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToDolarWesternUnionDtoDeserializer : IJsonDeserializer<DolarWesternUnionDto>
{
    private readonly IJsonValueReader _valueReader;

    public JsonToDolarWesternUnionDtoDeserializer(IJsonValueReader valueReader) 
        => _valueReader = valueReader;

    public Optional<DolarWesternUnionDto> Deserialize(JsonNode? node)
        => Optional<DolarWesternUnionDto>.Maybe(DeserializeIncludingFees(node) ?? DeserializeDefault(node));

    private DolarWesternUnionDto? DeserializeIncludingFees(JsonNode? node)
    {
        if (node is null)
        {
            return null;
        }

        JsonNode? service500node;
        if ((service500node = GetServiceNode(node, "500")) is null)
        {
            return null;
        }

        JsonNode? acPayGroupNode;
        if ((acPayGroupNode = GetPayGroupNode(service500node, "AC")) is null)
        {
            return null;
        }

        var strikeFxRate = GetNodeValue<double>(acPayGroupNode, "strike_fx_rate");
        if (!strikeFxRate.HasValue)
        {
            return null;
        }

        var grossFee = GetNodeValue<double>(acPayGroupNode, "gross_fee");

        return new DolarWesternUnionDto(strikeFxRate.Value, grossFee);
    }

    private DolarWesternUnionDto? DeserializeDefault(JsonNode? node)
    {
        var serviceNode = node?["categories"]?[0]?["services"]?[0];
        var strikeFxRate = GetNodeValue<double>(serviceNode, "strike_fx_rate");

        return strikeFxRate.HasValue
            ? new DolarWesternUnionDto(strikeFxRate.Value, Optional<double>.Empty)
            : null;
    }

    private JsonNode? GetServiceNode(JsonNode node, string serviceNumber)
    {
        var array = node["services_groups"] as JsonArray;
        var serviceNode = array?.SingleOrDefault(n => GetNodeValue<string>(n, "service").Equals(serviceNumber));

        return serviceNode;
    }

    private JsonNode? GetPayGroupNode(JsonNode? node, string fundIn)
    {
        var array = node?["pay_groups"] as JsonArray;
        var serviceNode = array?.SingleOrDefault(n => GetNodeValue<string>(n, "fund_in").Equals(fundIn));

        return serviceNode;
    }

    private Optional<T> GetNodeValue<T>(JsonNode? node, string property)
        => _valueReader.GetValue<T>(node, property);
}
