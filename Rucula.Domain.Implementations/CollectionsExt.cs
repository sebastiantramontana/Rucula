namespace Rucula.Domain.Implementations;

public static class CollectionsExt
{
    public static bool IsEmpty<T>(this IEnumerable<T> collection)
        => !(collection?.Any() ?? false);

    public static bool IsEmpty<T>(this List<T> collection)
        => (collection?.Count ?? 0) == 0;

    public static bool IsEmpty<T>(this T[] collection)
        => (collection?.Length ?? 0) == 0;

    public static bool IsEmpty<T>(this ICollection<T> collection)
        => (collection?.Count ?? 0) == 0;
}
