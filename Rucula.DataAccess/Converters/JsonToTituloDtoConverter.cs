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
            var parking = ConvertToParking(node);
            var moneda = ConverToMoneda(node);

            return new TituloDto(simbolo, precioCompra, precioVenta, parking, moneda);
        }

        private DateOnly ConvertToDate(JsonNode node, string key)
            => DateOnly.Parse(_valueReader.GetValue<string>(node, key));

        private Parking ConvertToParking(JsonNode node)
            => MapToParking(_valueReader.GetValue<string>(node, "settlementType"));

        private Parking MapToParking(string settlementType)
            => settlementType switch
            {
                "1" => Parking.CI,
                "2" => Parking.T24,
                "3" => Parking.T48,
                _ => throw new NotImplementedException($"Valor inválido para parking: {settlementType}")
            };

        private Moneda ConverToMoneda(JsonNode node)
            => MapToMoneda(_valueReader.GetValue<string>(node, "denominationCcy"));

        private Moneda MapToMoneda(string denominationCcy)
            => denominationCcy switch
            {
                "ARS" => Moneda.Peso,
                "EXT" => Moneda.DolarCable,
                "USD" => Moneda.DolarMep,
                _ => throw new NotImplementedException($"Valor inválido para moneda: {denominationCcy}")
            };

    }
}
