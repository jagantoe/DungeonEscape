namespace DungeonEscape.Logic;
public sealed class OpenDoor : Tile
{
	public OpenDoor()
	{
		Name = "Open Door";
		Description = "An open door, perhaps it leads to freedom...";
		DetailedDescription = "An open door, perhaps it leads to freedom... or your DOOM!";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
}
public abstract class Door : Tile, IInteract
{
	public Item Key { get; set; }
	public Door(string color, Item key)
	{
		Name = $"{color} Door";
		Description = $"A closed {color} door";
		DetailedDescription = $"Where there is a {color} door there must be a {color} key";
		Walkable = false;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
		Key = key;
	}
	static Door()
	{
		Tiles.ConfigTile(TileType.OpenDoor, new OpenDoor());
		Tiles.ConfigTile(TileType.RedDoor, new RedDoor());
		Tiles.ConfigTile(TileType.BlueDoor, new BlueDoor());
		Tiles.ConfigTile(TileType.GreenDoor, new GreenDoor());
		Tiles.ConfigTile(TileType.YellowDoor, new YellowDoor());
		Tiles.ConfigTile(TileType.PurpleDoor, new PurpleDoor());
		Tiles.ConfigTile(TileType.BlackDoor, new BlackDoor());
		Tiles.ConfigTile(TileType.WhiteDoor, new WhiteDoor());
		Tiles.ConfigTile(TileType.StateDoor, new StateDoor());
	}
	public static void Init() { }

	public ActResult Interact(Vector2 pos, Player player, Map map)
	{
		if (player.Items.Contains(Key))
		{
			map.ChangeState(pos, TileType.OpenDoor);
			return new SuccessResult("The door opens");
		}
		return new GeneralResult("Nothing happens, best to look for a key");
	}
}
public sealed class RedDoor : Door { public RedDoor() : base("Red", Item.Red_Key) { } }
public sealed class BlueDoor : Door { public BlueDoor() : base("Blue", Item.Blue_Key) { } }
public sealed class YellowDoor : Door { public YellowDoor() : base("Yellow", Item.Yellow_Key) { } }
public sealed class PurpleDoor : Door { public PurpleDoor() : base("Purple", Item.Purple_Key) { } }
public sealed class GreenDoor : Door { public GreenDoor() : base("Green", Item.Green_Key) { } }
public sealed class BlackDoor : Door { public BlackDoor() : base("Black", Item.Black_Key) { } }
public sealed class WhiteDoor : Door { public WhiteDoor() : base("White", Item.White_Key) { } }
public sealed class StateDoor : Tile, IInteract
{
	public StateDoor()
	{
		Name = $"Door";
		Description = $"A closed door with no key hole";
		DetailedDescription = $"A closed door with no key hole, there must be some mechanism that opens it";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.PointOfInterest;
	}

	public ActResult Interact(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config?.Targets is null || config.Targets.None()) return ErrorResult.ConfigMissing(pos);
		var target = config.Targets.First();
		var targetConfig = map.GetConfig(target);
		if (targetConfig is null) return ErrorResult.TargetConfigMissing(pos, target);
		if (targetConfig.Active)
		{
			map.ChangeState(pos, TileType.OpenDoor);
			return new SuccessResult("The door opens");
		}
		return new GeneralResult("Nothing happens, there must be some way to open it");
	}
}