namespace Rucula.Domain.Entities;

public sealed record class WinningOption(string Name, string Info, double? DolarPrice)
{
    public static readonly WinningOption NoWinners = new("No hay ganador!", "\U0001f937", null);
}
