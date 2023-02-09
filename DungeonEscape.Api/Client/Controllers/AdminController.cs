using DungeonEscape.Api.Authentication;
using DungeonEscape.Api.GameManagement;
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

	[HttpGet]
	[Route("Token")]
	public async Task<IActionResult> GetTokenAsync(string name, string password, CancellationToken token)
	{
		//var user = await _context.User.FirstOrDefaultAsync(u => u.Name == name && u.Password == password, token);
		//if (user == null) return BadRequest();
		//var userToken = _tokenProvider.GenerateToken(user.Id);
		return Ok(null);
	}

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

	[HttpGet]
	public IActionResult GetGameState([FromQuery] string adminPassword, [FromQuery] int gameId)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		var game = _dataService.GetGame(gameId);
		return Ok(game);
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
	public IActionResult UnloadEverything([FromQuery] string adminPassword)
	{
		if (adminPassword != AdminPassword) return Unauthorized();
		_dataService.UnloadEverything();
		return Ok();
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