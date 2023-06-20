using DungeonEscape.Api.Authentication;
using DungeonEscape.Api.GameManagement;
using DungeonEscape.Game;
using DungeonEscape.Game.StorageDTO;
using DungeonEscape.Logic;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace DungeonEscape.Api.Client.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AdminController : Controller
{
	private readonly string AdminPassword = Secret.AdminCode;

	private readonly TokenProvider _tokenProvider;
	private readonly DataService _dataService;
	private readonly GameOptions _gameOptions;

	public AdminController(TokenProvider tokenProvider, DataService dataService, GameOptions gameOptions)
	{
		_tokenProvider = tokenProvider;
		_dataService = dataService;
		_gameOptions = gameOptions;
	}

	// General
	[HttpGet]
	public object GetEnumJson()
	{
		return Enum.GetValues<TileType>().ToDictionary(t => t.ToString(), t => (int)t);
	}


	// Token
	public record TokenRequest(string name, int gameId, int playerId);
	[HttpPost]
	[Route("Token")]
	public IActionResult CreateToken([FromQuery] string adminPassword, [FromBody] TokenRequest value)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var token = _tokenProvider.GenerateToken(value.gameId, value.playerId);
		return Ok(token);
	}
	[HttpPost]
	[Route("Tokens")]
	public IActionResult CreateTokens([FromQuery] string adminPassword, [FromBody] List<TokenRequest> values)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var results = values.Select(x => new { Name = x.name, GameId = x.gameId, PlayerId = x.playerId, Token = _tokenProvider.GenerateToken(x.gameId, x.playerId) });
		return Ok(results);
	}

	// Map
	[HttpGet]
	public IActionResult GetLoadedMap([FromQuery] string adminPassword)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var map = _dataService.GetMap();
		return Ok(map.Adapt<Dictionary<string, TileType>>());
	}
	[HttpPost]
	public IActionResult LoadMap([FromQuery] string adminPassword, [FromQuery] string map)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var result = _dataService.LoadMap(map);
		return Ok(result);
	}
	[HttpPost]
	public IActionResult LoadMapManual([FromQuery] string adminPassword, [FromBody] List<MapTile> tiles)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var result = _dataService.LoadMapManual(tiles);
		return Ok(result);
	}
	[HttpPost]
	public IActionResult GetLoadedMapCleanConfig([FromQuery] string adminPassword)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var map = _dataService.GetMap();
		var dict = new Dictionary<Vector2, TileConfig>();
		foreach (var tile in map)
		{
			var isConfig = Tiles.GetTile(tile.Value) is TileWithConfig;
			if (isConfig) dict.Add(tile.Key, new TileConfig());
		}
		return Ok(dict.Adapt<Dictionary<string, TileConfigDTO>>());
	}

	// Game
	[HttpGet]
	public IActionResult GetGameStorage([FromQuery] string adminPassword, [FromQuery] int gameId)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var game = _dataService.GetGame(gameId);
		return Ok(game?.GetGameStorage());
	}
	[HttpPost]
	public async Task<IActionResult> LoadGame([FromQuery] string adminPassword, [FromQuery] int gameId)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var result = await _dataService.LoadGame(gameId);
		return Ok(result);
	}
	[HttpPost]
	public IActionResult UnloadGame([FromQuery] string adminPassword, [FromQuery] int gameId)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var result = _dataService.UnloadGame(gameId);
		return Ok(result);
	}
	[HttpPost]
	public IActionResult SetGameActive([FromQuery] string adminPassword, [FromQuery] int gameId, [FromQuery] bool active)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var game = _dataService.GetGame(gameId);
		if (game is not null) game.Active = active;
		return Ok(game?.Active);
	}

	// Cache
	[HttpPost]
	public IActionResult UnloadEverything([FromQuery] string adminPassword)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		_dataService.UnloadEverything();
		return Ok();
	}

	// Options
	[HttpGet]
	public IActionResult GetRateLimit([FromQuery] string adminPassword)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		return Ok(_dataService.GetRateLimit());
	}
	[HttpGet]
	public IActionResult GetOptions([FromQuery] string adminPassword)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		return Ok(_gameOptions);
	}
	[HttpPost]
	public IActionResult SetRoundDelay([FromQuery] string adminPassword, [FromQuery] int delay)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		_gameOptions.DelayBetweenRounds = delay;
		return Ok();
	}
	[HttpPost]
	public IActionResult SetSaveTimer([FromQuery] string adminPassword, [FromQuery] int time)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		_gameOptions.SaveEveryXRounds = time;
		return Ok();
	}
}