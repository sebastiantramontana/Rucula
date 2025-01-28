namespace Rucula.Domain.Implementations;

internal static class CollectionsExt
{
    internal static bool IsEmpty<T>(this IEnumerable<T> collection)
        => !(collection?.Any() ?? false);

    internal static bool IsEmpty<T>(this List<T> collection)
        => (collection?.Count ?? 0) == 0;

    internal static bool IsEmpty<T>(this T[] collection)
        => (collection?.Length ?? 0) == 0;

    internal static bool IsEmpty<T>(this ICollection<T> collection)
        => (collection?.Count ?? 0) == 0;
}
