using DungeonEscape.Game.GameDTO;
using DungeonEscape.Logic;

namespace DungeonEscape.Api.GameManagement;

public class GameService
{
	private readonly DataService _dataService;
	public GameService(DataService dataService)
	{
		_dataService = dataService;
	}

	public IEnumerable<PlayerActionResultDTO> ProcessGames()
	{
		var games = _dataService.GetGames();
		foreach (var game in games)
		{
			if (game.Active is false) continue;
			game.ResolvePlayerActions();
		}
		return games.SelectMany(x => x.GetPlayerActionResults());
	}

	public void AddAction(int gameId, PlayerAction action)
	{
		var game = _dataService.GetGame(gameId);
		if (game == null) return;
		game.AddPlayerAction(action);
	}
}
