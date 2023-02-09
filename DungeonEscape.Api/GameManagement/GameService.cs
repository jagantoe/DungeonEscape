using DungeonEscape.Logic;

namespace DungeonEscape.Api.GameManagement;

public class GameService
{
	private readonly DataService _dataService;
	public GameService(DataService dataService)
	{
		_dataService = dataService;
	}

	public void AddAction(int gameId, PlayerAction action)
	{
		var game = _dataService.GetGame(gameId);
		if (game == null) return;
		game.AddPlayerAction(action);
	}
}
