using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Converters
{
    internal class JsonToTituloDtoConverter : IJsonConverter<TituloDto>
    {
        private readonly IJsonValueReader _valueReader;

        public JsonToTituloDtoConverter(IJsonValueReader valueReader)
        {
            _valueReader = valueReader;
        }

        public TituloDto Convert(JsonNode node)
        {
            var simbolo = _valueReader.GetValue<string>(node, "symbol");
            var precioCompra = _valueReader.GetValue<double>(node, "bidPrice");
            var precioVenta = _valueReader.GetValue<double>(node, "offerPrice");
            var parking = _valueReader.GetValue<string>(node, "settlementType");
            var moneda = _valueReader.GetValue<string>(node, "denominationCcy");

            return new TituloDto(simbolo, precioCompra, precioVenta, parking, moneda);
        }
    }
}
