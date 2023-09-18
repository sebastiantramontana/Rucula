using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal class JsonToTituloDetailsDtoDeserializer : IJsonDeserializer<TituloDetailsDto>
    {
        private readonly IJsonValueReader _valueReader;

        public JsonToTituloDetailsDtoDeserializer(IJsonValueReader jsonValueReader)
        {
            _valueReader = jsonValueReader;
        }

        public TituloDetailsDto Deserialize(JsonNode node)
        {
            var isin = _valueReader.GetValue<string>(node, "codigoIsin");
            var denominacion = _valueReader.GetValue<string>(node, "denominacion");
            var tipoObligacion = _valueReader.GetValue<string>(node, "tipoObligacion");
            var fechaVencimiento = _valueReader.GetValue<string>(node, "fechaVencimiento");

            return new TituloDetailsDto(isin, denominacion, tipoObligacion, fechaVencimiento);
        }
    }
}
