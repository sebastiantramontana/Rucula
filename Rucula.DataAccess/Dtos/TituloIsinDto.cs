namespace Rucula.DataAccess.Dtos
{
    public record class TituloIsinDto(string CodigoIsin,
                                      string Denominacion,
                                      TituloDto TituloCable,
                                      TituloDto TituloPeso,
                                      TituloDto TituloMep,
                                      DateOnly Vencimiento);
}
