namespace DungeonEscape.Logic;
public static class UtilExtensions
{
	public static bool IsType<TType>(this object obj, out TType type) where TType : class
	{
		type = obj as TType;
		return type != null;
	}

	public static bool None<T>(this IEnumerable<T> enumerable)
	{
		return !enumerable.Any();
	}

	public static void Cycle<T>(this ICollection<T> collection)
	{
		var first = collection.First();
		collection.Remove(first);
		collection.Add(first);
	}

	public static bool MissingConfigTarget(this TileConfig? config)
	{
		return config?.Targets is null || config.Targets.None();
	}
}
