using DungeonEscape.Game;
using DungeonEscape.Game.StorageDTO;
using DungeonEscape.Logic;
using GitHubGistStorage;
using Mapster;
using Microsoft.Extensions.Caching.Memory;
using Octokit;
using System.Numerics;
using System.Text.Json;

namespace DungeonEscape.Api.GameManagement;

public class DataService
{
	private static string LOADEDMAP_CACHE = "LOADEDMAP";
	private static string MAP_CACHE = "MAP";

	private static string GAMES_CACHE = "GAMES";
	private static string GAME_CACHE = "Game-";

	private readonly IMemoryCache _memoryCache;
	private readonly IGistClient _gistClient;

	public DataService(IMemoryCache memoryCache, IGistClient gistClient)
	{
		_memoryCache = memoryCache;
		_gistClient = gistClient;
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
		var currentMap = GetMap();
		if (currentMap != null) return "A map is already loaded please clear everything before loading a map";
		var map = MapLoader.LoadMap(mapName);
		if (map == null) return "Map not found";
		if (map.Count == 0) return "Map is empty";
		if (map.Count(x => x.Value == TileType.Start) != 1) return "Map needs to have exactly 1 start";
		_memoryCache.Set(MAP_CACHE, map);
		_memoryCache.Set(LOADEDMAP_CACHE, mapName);
		return "Map loaded successfully";
	}
	public string LoadMapManual(List<MapTile> tiles)
	{
		var currentMap = _memoryCache.GetValueOrDefault<Dictionary<Vector2, TileType>>(MAP_CACHE);
		if (currentMap != null) return "A map is already loaded please clear everything before loading a map";
		if (tiles.Any(tile => !Enum.IsDefined(tile.Value))) return "Invalid value detected";
		if (tiles.Count != tiles.DistinctBy(tile => new { tile.X, tile.Y }).Count()) return "Duplicate tile detected";
		if (tiles.Count(x => x.Value == TileType.Start) != 1) return "Map needs to have exactly 1 start";
		var map = tiles.Adapt<Dictionary<Vector2, TileType>>();
		if (map == null) return "Failed to load map";
		if (map.Count == 0) return "Map is empty";
		_memoryCache.Set(MAP_CACHE, map);
		_memoryCache.Set(LOADEDMAP_CACHE, "Manual");
		return "Map loaded successfully";
	}

	public List<GameState> GetGames()
	{
		var loadedgames = GetLoadedGames();
		var games = new List<GameState>();
		foreach (var game in loadedgames)
		{
			var g = GetGame(game);
			if (g is not null) games.Add(g);
		}
		return games;
	}
	public GameState? GetGame(int id)
	{
		return _memoryCache.Get<GameState>(GAME_CACHE + id);
	}
	public async Task<string> LoadGame(int id)
	{
		var map = GetLoadedMap();
		if (map == null) return "Map is not loaded";
		var currentGame = GetGame(id);
		if (currentGame is not null) return "This game is already loaded please clear it before reloading it";
		var game = await LoadGameStateFromStorage(id);
		if (game is null) return "Game not found";
		if (game.MapName != map) return "The game you are trying to load does not match the loaded map";
		if (game.Players.Count == 0) return "Game has no players";
		else if (game.Players.DistinctBy(x => x.Id).Count() != game.Players.Count) return "Duplicate player id found";
		game.Active = false;
		_memoryCache.Set(GAME_CACHE + id, game);
		GetLoadedGames().Add(id);
		return "Game loaded successfully";
	}
	public string UnloadGame(int id)
	{
		var game = GetGame(id);
		if (game is null) return "Game not found";
		_memoryCache.Remove(GAME_CACHE + id);
		GetLoadedGames().Remove(id);
		return "Game unloaded successfully";
	}

	private HashSet<int> GetLoadedGames()
	{
		var games = _memoryCache.Get<HashSet<int>>(GAMES_CACHE);
		if (games is null)
		{
			games = new HashSet<int>();
			_memoryCache.Set(GAMES_CACHE, games);
		}
		return games;
	}

	private async Task<GameState> LoadGameStateFromStorage(int id)
	{
		var storageId = $"DungeonEscape-{id}";
		var dictionary = await _gistClient.GetGistDictionary();
		var storedGame = dictionary.GetValueOrDefault(storageId);
		if (storedGame is null) return null;
		var gameStorage = JsonSerializer.Deserialize<GameStorageDTO>(storedGame);
		if (gameStorage is null) return null;
		var map = GetMap();
		var gameState = new GameState(map, gameStorage);
		return gameState;
	}
	public async Task SaveGameStateToStorage(GameStorageDTO game)
	{
		var storageId = $"DungeonEscape-{game.Id}";
		await _gistClient.UpdateGist(storageId, JsonSerializer.Serialize(game));
	}

	public RateLimit GetRateLimit()
	{
		return _gistClient.GetRateLimit();
	}
}