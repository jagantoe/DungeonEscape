using DungeonEscape.Game;
using DungeonEscape.Logic;
using Microsoft.Extensions.Caching.Memory;
using System.Numerics;

namespace DungeonEscape.Api.GameManagement;

public class DataService
{
	private static string LOADEDMAP_CACHE = "LOADEDMAP";
	private static string MAP_CACHE = "MAP";
	private static string GAME_CACHE = "Game-";

	private readonly IMemoryCache _memoryCache;

	public DataService(IMemoryCache memoryCache)
	{
		_memoryCache = memoryCache;
	}

	public void UnloadEverything()
	{
		((MemoryCache)_memoryCache).Clear();
	}

	public string GetLoadedMap()
	{
		return _memoryCache.Get<string>(LOADEDMAP_CACHE);
	}
	public Dictionary<Vector2, TileType>? GetMap()
	{
		return _memoryCache.Get<Dictionary<Vector2, TileType>>(MAP_CACHE);
	}
	public string LoadMap(string mapName)
	{
		var currentMap = _memoryCache.GetValueOrDefault<Dictionary<Vector2, TileType>>(MAP_CACHE);
		if (currentMap != null) return "A map is already loaded please clear everything before loading a map";
		var map = MapLoader.LoadMap(mapName);
		if (map == null) return "Map not found";
		if (map.Count == 0) return "Map is empty";
		_memoryCache.Set(MAP_CACHE, map);
		_memoryCache.Set(LOADEDMAP_CACHE, mapName);
		return "Map loaded successfully";
	}

	public GameState? GetGame(int id)
	{
		return _memoryCache.Get<GameState>(GAME_CACHE + id);
	}
	public async Task<string> LoadGame(int id)
	{
		var map = GetLoadedMap();
		if (map == null) return "Map is not loaded";
		var currentGame = _memoryCache.GetValueOrDefault<Dictionary<Vector2, TileType>>(GAME_CACHE + id);
		if (currentGame != null) return "This game is already loaded please clear it before reloading it";
		var game = await LoadGameStateFromStorage(id);
		if (game == null) return "Game not found";
		if (game.MapName != map) return "The game you are trying to load does not match the loaded map";
		if (game.Players.Count == 0) return "Game has no players";
		_memoryCache.Set(GAME_CACHE + id, game);
		return "Game loaded successfully";
	}
	public string UnloadGame(int id)
	{
		return "ok";
		//var map = GetLoadedMap();
		//if (map == null) return "Map is not loaded";
		//var currentGame = _memoryCache.GetValueOrDefault<Dictionary<Vector2, TileType>>(GAME_CACHE + id);
		//if (currentGame != null) return "This game is already loaded please clear it before reloading it";
		//var game = await LoadGameStateFromStorage(id);
		//if (game == null) return "Game not found";
		//if (game.MapName != map) return "The game you are trying to load does not match the loaded map";
		//if (game.Players.Count == 0) return "Game has no players";
		//_memoryCache.Set(GAME_CACHE + id, game);
		//return "Game loaded successfully";
	}

	private async Task<GameState> LoadGameStateFromStorage(int id)
	{
		return null;
	}
}