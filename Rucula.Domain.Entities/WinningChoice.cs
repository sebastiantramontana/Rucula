namespace Rucula.Domain.Entities;

public sealed record class WinningChoice(string Name, string Info, double? DolarPrice)
{
    public static readonly WinningChoice NoWinners = new("No hay ganador!", "\U0001f937", null);
}
