namespace DungeonEscape.Logic;
public sealed class Floor : Tile
{
	public static string Name = "Floor";
	public static string Description = "A flat floor";

	public Floor()
	{
		base.Name = Name;
		base.Description = Description;
		DetailedDescription = Description;
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	static Floor()
	{
		Tiles.ConfigTile(TileType.Floor, new Floor());
		Tiles.ConfigTile(TileType.SmokeFloor, new SmokeFloor());
		Tiles.ConfigTile(TileType.Teleporter, new Teleporter());
		Tiles.ConfigTile(TileType.LeapLedge, new LeapLedge());
		Tiles.ConfigTile(TileType.PressurePlate, new PressurePlate());
		Tiles.ConfigTile(TileType.FalsePressurePlate, new FalsePressurePlate());
		Tiles.ConfigTile(TileType.PressurePlateReseter, new PressurePlateReseter());
		Tiles.ConfigTile(TileType.PressurePlateChecker, new PressurePlateChecker());
		Tiles.ConfigTile(TileType.ToggleFloorOn, new ToggleFloorOn());
		Tiles.ConfigTile(TileType.ToggleFloorOff, new ToggleFloorOff());
		Tiles.ConfigTile(TileType.TileToggleChecker, new TileToggleChecker());
	}
	public static void Init() { }
}
public sealed class SmokeFloor : Tile
{
	public SmokeFloor()
	{
		Name = Floor.Name;
		Description = Floor.Description;
		DetailedDescription = "A flat floor with a sigil on it, it looks like an eye of some sort";
		Walkable = true;
		BlocksVision = true;
		TileKind = TileKind.Walkable;
	}
}
public class Teleporter : TileWithConfig, IOnEnter
{
	public Teleporter()
	{
		Name = Floor.Name;
		Description = "A flat floor with a sigil of some sort";
		DetailedDescription = "A flat floor with a sigil of some sort";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config.MissingConfigTarget()) return ErrorResult.ConfigMissing(pos);
		player.Position = config!.FirstTarget();
		return new GeneralResult("With a flash and a poof you find yourself somewhere new");
	}
}
public class LeapLedge : TileWithConfig, IInteract
{
	public LeapLedge()
	{
		Name = "Ledge";
		Description = "The ledge of a pit";
		DetailedDescription = "The ledge of a pit, an athletic person should be able to jump to the other side";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public ActResult Interact(Vector2 pos, Player player, Map map)
	{
		if (player.Character is PlayerCharacter.StrongMan)
		{
			var config = map.GetConfig(pos);
			if (config.MissingConfigTarget()) return ErrorResult.ConfigMissing(pos);
			player.Position = config!.FirstTarget();
			return new GeneralResult("You leap to the other side of the pit");
		}
		return new GeneralResult("You're standing on the ledge of a pit, be careful not to fall in");
	}
}
public class PressurePlate : TileWithConfig, IOnEnter
{
	public PressurePlate()
	{
		Name = Floor.Name;
		Description = "Some sort of pressure plate";
		DetailedDescription = "Some sort of pressure plate";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config is null) config = map.AddConfigAt(pos);
		if (config.Active) return new GeneralResult("*silence*");
		config.Active = true;
		return new GeneralResult("*click*");
	}
}
public class FalsePressurePlate : Tile, IOnEnter
{
	public FalsePressurePlate()
	{
		Name = Floor.Name;
		Description = "Some sort of pressure plate";
		DetailedDescription = "Some sort of pressure plate";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var surroundingTiles = Vision.GetVisionRing(1).Select(x => x + pos);
		foreach (var tile in surroundingTiles)
		{
			var config = map.GetConfig(tile);
			if (config is not null) config.Active = false;
		}
		return new GeneralResult("*silence*");
	}
}
public class PressurePlateReseter : TileWithConfig, IOnEnter
{
	public PressurePlateReseter()
	{
		Name = Floor.Name;
		Description = "Some sort of pressure plate, it has a marking of a circular arrow";
		DetailedDescription = "Some sort of pressure plate, it has a marking of a circular arrow";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config.MissingConfigTarget()) return ErrorResult.ConfigMissing(pos);
		foreach (var target in config!.Targets)
		{
			var targetConfig = map.GetConfig(target);
			if (targetConfig is null) targetConfig = map.AddConfigAt(target);
			targetConfig.Active = false;
		}
		return new GeneralResult("Loud clicks echo from inside the room");
	}
}
public class PressurePlateChecker : TileWithConfig, IOnEnter
{
	public PressurePlateChecker()
	{
		Name = Floor.Name;
		Description = "Some sort of pressure plate";
		DetailedDescription = "Some sort of pressure plate";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config.MissingConfigTarget()) return ErrorResult.ConfigMissing(pos);
		foreach (var target in config!.Targets)
		{
			var targetConfig = map.GetConfig(target);
			if (targetConfig is null) targetConfig = map.AddConfigAt(target);
			if (targetConfig.Active == false) return new GeneralResult("*silence*");
		}
		config.Active = true;
		return new SuccessResult("You hear a door unlocking across the room");
	}
}
public class TileToggleChecker : TileWithConfig, IOnEnter
{
	public TileToggleChecker()
	{
		Name = Floor.Name;
		Description = "Some sort of pressure plate";
		DetailedDescription = "Some sort of pressure plate";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config.MissingConfigTarget()) return ErrorResult.ConfigMissing(pos);
		foreach (var target in config!.Targets)
		{
			var targetTile = map.GetTileAt(target);
			if (targetTile is null) return ErrorResult.TargetConfigMissing(pos, target);
			if (targetTile is not TileType.ToggleFloorOn) return new GeneralResult("*silence*");
		}
		config.Active = true;
		return new SuccessResult("You hear a door unlocking across the room");
	}
}
public class ToggleFloorOn : Tile
{
	public ToggleFloorOn()
	{
		Name = "Tile";
		Description = "A white tile";
		DetailedDescription = "A white tile, it seems to give off some light";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}
}
public class ToggleFloorOff : Tile
{
	public ToggleFloorOff()
	{
		Name = "Tile";
		Description = "A black tile";
		DetailedDescription = "A black tile, devoid of light";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
}