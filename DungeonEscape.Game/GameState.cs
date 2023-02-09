using DungeonEscape.Game.StorageDTO;
using DungeonEscape.Logic;
using Mapster;
using System.Numerics;

namespace DungeonEscape.Game;
public class GameState
{
	public int Id { get; set; }
	public bool Active { get; set; }
	public int CurrentRound { get; set; }
	public string MapName { get; set; }

	public Map Map { get; set; }
	public List<Player> Players { get; set; }

	public Dictionary<int, (PlayerAction, DateTime)> PlayerActions { get; set; } = new Dictionary<int, (PlayerAction, DateTime)>();
	private bool Processing = false;

	public GameState(Dictionary<Vector2, TileType> map, GameStorageDTO gameStorage)
	{
		Id = gameStorage.Id;
		Active = gameStorage.Active;
		CurrentRound = gameStorage.CurrentRound;
		Players = gameStorage.Players.Adapt<List<Player>>();
		Map = new Map(map, gameStorage.Adapt<Dictionary<Vector2, TileType?>>());
	}

	public void ResolvePlayerActions()
	{
		Processing = true;
		var actions = GetPlayerActionsInOrder();
		foreach (var action in actions)
		{
			var player = Players.First(x => x.Id == action.PlayerId);

		}
		Processing = false;
	}

	private List<PlayerAction> GetPlayerActionsInOrder()
	{
		var actions = PlayerActions.Values.OrderBy(x => x.Item2).Select(x => x.Item1).ToList();
		PlayerActions.Clear();
		return actions;
	}
	public void AddPlayerAction(PlayerAction action)
	{
		if (Processing) return;
		PlayerActions[action.PlayerId] = (action, DateTime.Now);
	}

	public GameStorageDTO GetGameStorage()
	{
		return new()
		{
			Id = Id,
			Active = Active,
			CurrentRound = CurrentRound,
			MapName = MapName,
			Players = Players.Adapt<List<PlayerStorageDTO>>(),
			GameState = Map.MapState.Adapt<Dictionary<string, TileType>>()
		};
	}
}
