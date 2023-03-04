namespace DungeonEscape.Game;
public class GameState
{
	public int Id { get; set; }
	public bool Active { get; set; }
	public int CurrentRound { get; set; }
	public string MapName { get; set; }

	public Map Map { get; set; }
	public List<Player> Players { get; set; }

	private bool Processing = false;
	public Dictionary<int, (PlayerAction, DateTime)> PlayerActions { get; set; } = new Dictionary<int, (PlayerAction, DateTime)>();
	public List<PlayerActionResultDTO> PlayerActionResults { get; set; } = new List<PlayerActionResultDTO>();
	public List<PlayerInspection> PlayerInspections { get; set; } = new List<PlayerInspection>();

	public GameState(Dictionary<Vector2, TileType> map, GameStorageDTO gameStorage)
	{
		Id = gameStorage.Id;
		Active = gameStorage.Active;
		CurrentRound = gameStorage.CurrentRound;
		MapName = gameStorage.MapName;
		Players = gameStorage.Players.Adapt<List<Player>>();
		Map = new Map(map, gameStorage.Adapt<Dictionary<Vector2, TileType?>>());
	}

	private void RoundReset()
	{
		PlayerActionResults.Clear();
		PlayerInspections.Clear();
	}

	public void ResolvePlayerActions()
	{
		Processing = true;
		RoundReset();
		var actions = GetPlayerActionsInOrder();
		foreach (var action in actions)
		{
			var actionResults = new Queue<ActResult>();
			var player = Players.First(x => x.Id == action.PlayerId);
			actionResults.Enqueue(new ActionResult(action.ToString()!));
			var standingTile = Map[player.Position];
			// Player invalid position check
			if (standingTile is null)
			{
				player.Position = Map.Start;
				actionResults.Enqueue(new ErrorResult("Invalid player position detected, current action canceled and resetting player"));
				continue;
			}
			// Execute action
			switch (action)
			{
				case MoveAction a:
					Move(a.Target);
					break;
				case InspectAction a:
					Inspect(a.Target);
					break;
				case InteractAction a:
					Interact(a.Target);
					break;
				case ResetAction:
					Reset();
					break;
				case SwitchCharacterAction a:
					SwitchCharacter(a.Character);
					break;
				default:
					actionResults.Enqueue(new ErrorResult("This should not be possible please inform host if you see this"));
					break;
			}
			// Dead Check
			if (player.IsDead)
			{
				player.Revive(Map.Start);
				actionResults.Enqueue(new DeathResult(Quotes.GetDeathQuote()));
			}
			// Add action results 
			PlayerActionResults.Add(new PlayerActionResultDTO { PlayerId = player.Id, ActionResults = actionResults });

			void Move(Vector2 target)
			{
				if (Vision.MovementGrid.Select(x => player.Position).Contains(target))
				{
					player.Position = target;
					var targetTile = Map[target];
					actionResults.Enqueue(new SuccessResult($"Moved to {target}"));
					if (targetTile.IsType<IOnEnter>(out var tile))
					{
						var results = tile.OnEnter(player, Map);
						actionResults.Enqueue(results);
					}
				}
				else
				{
					actionResults.Enqueue(new ErrorResult("Target is not in range."));
				}
			}
			void Inspect(Vector2 target)
			{
				if (Vision.GetVisionGrid(1).Select(x => x * player.Position).Contains(target))
				{
					var targetTile = Map[target];
					var inspection = targetTile.Inspect(player.Character == PlayerCharacter.Archeologist);
					PlayerInspections.Add(new PlayerInspection { PlayerId = player.Id, Inspection = inspection });
					actionResults.Enqueue(new SuccessResult($"Name:{inspection.Name} - Description: {inspection.Description}"));
				}
				else
				{
					actionResults.Enqueue(new ErrorResult("Target is not in range."));
				}
			}
			void Interact(Vector2 target)
			{
				if (Vision.GetVisionGrid(1).Select(x => x * player.Position).Contains(target))
				{
					var targetTile = Map[target];
					if (targetTile.IsType<IInteract>(out var tile))
					{
						tile.Interact(target, player, Map);
					}
					else
					{
						actionResults.Enqueue(new GeneralResult("Action to interact with {target}"));
					}
				}
				else
				{
					actionResults.Enqueue(new ErrorResult("Target is not in range."));
				}
			}
			void SwitchCharacter(PlayerCharacter character)
			{
				if (standingTile is Start)
				{
					player.Character = character;
					player.Revive(Map.Start);
					actionResults.Enqueue(new SuccessResult($"Switched to {character}"));
				}
				else
				{
					actionResults.Enqueue(new ErrorResult("You can only switch characters while on the start tile!"));
				}
			}
			void Reset()
			{
				player.Reset(Map.Start);
				actionResults.Enqueue(new DeathResult(Quotes.GetResetQuote()));
			}
		}

		// Interaction check
		var player1 = Players[0];
		var target1 = new Vector2();

		Processing = false;
	}

	public IEnumerable<PlayerGameStateDTO> GetPlayerGameStates()
	{
		var states = new List<PlayerGameStateDTO>();
		foreach (var player in Players)
		{
			states.Add(GetPlayerGameState(player));
		}
		return states;
		PlayerGameStateDTO GetPlayerGameState(Player player)
		{
			int size;
			var tile = Map[player.Position];
			if (tile.BlocksVision) size = 0;
			else if (player.Character == PlayerCharacter.Explorer) size = 2;
			else size = 1;

			var vision = Vision.GetVisionGrid(size).Select(x => new { PositionX = x.X, PositionY = x.Y, TileKind = Map[x]! }).Adapt<List<TileDTO>>();
			return new PlayerGameStateDTO
			{
				Player = player.Adapt<PlayerDTO>(),
				Vision = vision,
			};
		}
	}
	public IEnumerable<PlayerInspection> GetInspections()
	{
		return PlayerInspections;
	}
	public IEnumerable<PlayerActionResultDTO> GetPlayerActionResults()
	{
		return PlayerActionResults;
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
