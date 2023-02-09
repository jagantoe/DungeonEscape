using Microsoft.Extensions.Caching.Memory;

namespace DungeonEscape.Api.GameManagement;

public static class Extensions
{
	public static T GetValueOrDefault<T>(this IMemoryCache memoryCache, string key)
	{
		memoryCache.TryGetValue(key, out T value);
		return value;
	}
}
