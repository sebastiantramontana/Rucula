using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal sealed class JsonToTituloDetailsDtoDeserializer(IJsonValueReader jsonValueReader) : IJsonDeserializer<TituloDetailsDto>
{
    public Optional<TituloDetailsDto> Deserialize(JsonNode? node)
    {
        if (node is null)
        {
            return Optional<TituloDetailsDto>.Empty;
        }

        var isin = jsonValueReader.GetRequiredValue<string>(node!, "codigoIsin");
        var denominacion = jsonValueReader.GetRequiredValue<string>(node!, "denominacion");
        var tipoObligacion = jsonValueReader.GetRequiredValue<string>(node!, "tipoObligacion");
        var fechaVencimiento = jsonValueReader.GetRequiredValue<string>(node!, "fechaVencimiento");

        return Optional<TituloDetailsDto>.Sure(new TituloDetailsDto(isin, denominacion, tipoObligacion, fechaVencimiento));
    }
}
