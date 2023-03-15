using DungeonEscape.Api.Authentication;
using DungeonEscape.Api.GameManagement;
using DungeonEscape.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Numerics;

namespace DungeonEscape.Api.Client.SignalR;

[Authorize]
public class GameHub : Hub
{
	private readonly GameService _gameService;
	public GameHub(GameService gameService)
	{
		_gameService = gameService;
	}

	private int UserId => Context.User.GetUserId();
	private int GameId => Context.User.GetGameId();

	public override Task OnConnectedAsync() => Groups.AddToGroupAsync(Context.ConnectionId, UserId.ToString());

	public void Move(int x, int y)
	{
		_gameService.AddAction(GameId, new MoveAction(UserId, new Vector2(x, y)));
	}
	public void Interact(int x, int y)
	{
		_gameService.AddAction(GameId, new InteractAction(UserId, new Vector2(x, y)));
	}
	public void Inspect(int x, int y)
	{
		_gameService.AddAction(GameId, new InspectAction(UserId, new Vector2(x, y)));
	}
	public void SwitchCharacters(PlayerCharacter character)
	{
		_gameService.AddAction(GameId, new SwitchCharacterAction(UserId, character));
	}
	public void Reset()
	{
		_gameService.AddAction(GameId, new ResetAction(UserId));
	}
}