namespace Rucula.Domain.Entities
{
    public record class WinningChoice(string Name, string Info, double DolarPrice)
    {
        public static readonly WinningChoice NoWinners = new WinningChoice("No hay ganador!", "\U0001f937", double.NaN);
    }
}
