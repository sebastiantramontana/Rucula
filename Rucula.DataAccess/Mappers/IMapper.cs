namespace Rucula.DataAccess.Mappers
{
    internal interface IMapper<in TFrom, out TTo>
    {
        TTo Map(TFrom from);
    }
}
