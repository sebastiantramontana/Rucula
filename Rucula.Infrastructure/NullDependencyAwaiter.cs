namespace Rucula.Infrastructure;

public static class NullDependencyAwaiter
{
    public static async Task AwaitToNotNull<T>(Func<T> getDependency)
    {
        while (IsDependencyStillNull(getDependency))
        {
            await Task.Delay(1);
        }
    }

    private static bool IsDependencyStillNull<T>(Func<T> getDependency)
        => getDependency() is null;
}
