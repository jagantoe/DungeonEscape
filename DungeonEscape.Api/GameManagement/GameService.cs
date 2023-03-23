using DungeonEscape.Game.GameDTO;
using DungeonEscape.Logic;

namespace DungeonEscape.Api.GameManagement;

public record GameResult(IEnumerable<PlayerGameStateDTO> PlayerStates, IEnumerable<PlayerActionResultDTO> PlayerActions, IEnumerable<PlayerInspection> PlayerInspections);
public class GameService
{
	private readonly DataService _dataService;
	public GameService(DataService dataService)
	{
		_dataService = dataService;
	}

	public GameResult ProcessGames(int saveInterval)
	{
		// Get all games and filter for active
		var games = _dataService.GetGames().Where(x => x.Active);
		foreach (var game in games)
		{
			game.ResolvePlayerActions();
			if (game.CurrentRound % saveInterval == 0)
			{
				_dataService.SaveGameStateToStorage(game.GetGameStorage());
			}
		}
		return new GameResult(games.SelectMany(x => x.GetPlayerGameStates()), games.SelectMany(x => x.GetPlayerActionResults()), games.SelectMany(x => x.GetInspections()));
	}

	public void AddAction(int gameId, PlayerAction action)
	{
		var game = _dataService.GetGame(gameId);
		if (game == null) return;
		game.AddPlayerAction(action);
	}
}
