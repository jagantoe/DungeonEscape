using DungeonEscape.Api.Client.SignalR;
using DungeonEscape.Api.GameManagement;
using Microsoft.AspNetCore.SignalR;

namespace DungeonEscape.Api.BackgroundServices;

public class GameBackgroundService : BackgroundService
{
	private readonly IHubContext<GameHub> _hubContext;
	private readonly IHubContext<DashboardHub> _dashboardContext;
	private readonly GameOptions _gameOptions;
	private readonly GameService _gameService;

	public GameBackgroundService(IHubContext<GameHub> hubContext, IHubContext<DashboardHub> dashboardContext, GameOptions gameOptions, GameService gameService)
	{
		_hubContext = hubContext;
		_dashboardContext = dashboardContext;
		_gameOptions = gameOptions;
		_gameService = gameService;
	}

	protected async override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			// Default delay
			Task delay = Task.Delay(10000);
			var results = _gameService.ProcessGames(_gameOptions.SaveEveryXRounds);
			// Send out player action results
			foreach (var result in results.PlayerStates) _ = _hubContext.Clients.Group(result.Player.Id.ToString()).SendAsync("NewRound", result);
			foreach (var result in results.PlayerInspections) _ = _hubContext.Clients.Group(result.PlayerId.ToString()).SendAsync("Inspect", result);
			foreach (var result in results.PlayerActions) _ = _dashboardContext.Clients.Group(result.PlayerId.ToString()).SendAsync("Game", result);

			delay = Task.Delay(_gameOptions.DelayBetweenRounds);
			await delay;
		}
	}
}