namespace Rucula.Infrastructure;

public static class Awaiter
{
    public static async Task KeepAwaiting(Func<bool> canContinue)
    {
        while (!canContinue())
        {
            await Task.Delay(1);
        }
    }

    public static async Task AwaitToDependencyNotNull<T>(Func<T> getDependency)
    {
        while (IsDependencyStillNull(getDependency))
        {
            await Task.Delay(1);
        }
    }

    private static bool IsDependencyStillNull<T>(Func<T> getDependency)
        => getDependency() is null;
}
