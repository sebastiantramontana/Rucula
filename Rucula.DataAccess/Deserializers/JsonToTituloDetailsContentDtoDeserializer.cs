using Rucula.DataAccess.Dtos;
using System.Text.Json.Nodes;

namespace Rucula.DataAccess.Deserializers
{
    internal class JsonToTituloDetailsContentDtoDeserializer : IJsonDeserializer<TituloDetailsContentDto>
    {
        public JsonToTituloDetailsContentDtoDeserializer(IJsonDeserializer<PaginationDto> paginationDeserializer, JSon)
        {
        }

        public TituloDetailsContentDto Deserialize(JsonNode node)
        {
            return new TituloDetailsContentDto()
        }
    }
}
