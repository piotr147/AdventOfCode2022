namespace AdventOfCode2022.Utilities
{
    public static class CollectionExtensions
    {
        public static IEnumerable<int?> ToNullableInt(this IEnumerable<string> source) =>
            source.Select<string, int?>(s => int.TryParse(s, out int number)
                ? number
                : null
            );
    }
}
