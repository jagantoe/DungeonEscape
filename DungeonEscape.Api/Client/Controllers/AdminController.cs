using DungeonEscape.Api.Authentication;
using DungeonEscape.Api.GameManagement;
using DungeonEscape.Game;
using DungeonEscape.Logic;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace DungeonEscape.Api.Client.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AdminController : Controller
{
	private static readonly string AdminPassword = "*** secret admin password***";

	private readonly TokenProvider _tokenProvider;
	private readonly DataService _dataService;
	private readonly GameService _gameService;
	private readonly GameOptions _gameOptions;

	public AdminController(TokenProvider tokenProvider, DataService dataService, GameService gameService, GameOptions gameOptions)
	{
		_tokenProvider = tokenProvider;
		_dataService = dataService;
		_gameService = gameService;
		_gameOptions = gameOptions;
	}

	// Token
	[HttpGet]
	[Route("Token")]
	public IActionResult GetToken([FromQuery] string adminPassword, [FromQuery] (int gameId, int playerId) value)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var token = _tokenProvider.GenerateToken(value.gameId, value.playerId);
		return Ok(token);
	}
	[HttpGet]
	[Route("Tokens")]
	public IActionResult GetTokens([FromQuery] string adminPassword, [FromQuery] List<(int gameId, int playerId)> values)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var results = values.Select(x => new { GameId = x.gameId, PlayerId = x.playerId, Token = _tokenProvider.GenerateToken(x.gameId, x.playerId) });
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

	// Game
	[HttpGet]
	public IActionResult GetGameState([FromQuery] string adminPassword, [FromQuery] int gameId)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var game = _dataService.GetGame(gameId);
		return Ok(game);
	}
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