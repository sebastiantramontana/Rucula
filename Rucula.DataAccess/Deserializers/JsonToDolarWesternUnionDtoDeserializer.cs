using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal class JsonToDolarWesternUnionDtoDeserializer : IJsonDeserializer<DolarWesternUnionDto>
    {
        private readonly IJsonValueReader _valueReader;

        public JsonToDolarWesternUnionDtoDeserializer(IJsonValueReader valueReader)
        {
            _valueReader = valueReader;
        }

        public DolarWesternUnionDto Deserialize(JsonNode node)
        {
            var serviceNode = node["categories"]?[0]?["services"]?[0];

            double? strike_fx_rate = (serviceNode is not null)
                ? _valueReader.GetValue<double>(serviceNode, "strike_fx_rate")
                : null;

            return new DolarWesternUnionDto(strike_fx_rate);
        }
    }
}
