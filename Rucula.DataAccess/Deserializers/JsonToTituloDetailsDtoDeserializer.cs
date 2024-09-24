using Rucula.DataAccess.Dtos;
using Rucula.Domain.Entities;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers;

internal class JsonToTituloDetailsDtoDeserializer : IJsonDeserializer<TituloDetailsDto>
{
    private readonly IJsonValueReader _valueReader;

    public JsonToTituloDetailsDtoDeserializer(IJsonValueReader jsonValueReader)
    {
        _valueReader = jsonValueReader;
    }

    public Optional<TituloDetailsDto> Deserialize(JsonNode? node)
    {
        if (node is null)
            return Optional<TituloDetailsDto>.Empty;

        var isin = _valueReader.GetRequiredValue<string>(node!, "codigoIsin");
        var denominacion = _valueReader.GetRequiredValue<string>(node!, "denominacion");
        var tipoObligacion = _valueReader.GetRequiredValue<string>(node!, "tipoObligacion");
        var fechaVencimiento = _valueReader.GetRequiredValue<string>(node!, "fechaVencimiento");

        return Optional<TituloDetailsDto>.Sure(new TituloDetailsDto(isin, denominacion, tipoObligacion, fechaVencimiento));
    }
}
