namespace DungeonEscape.Logic;
public sealed class Floor : Tile
{
	public Floor()
	{
		Name = "Floor";
		Description = "A flat floor";
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
		Tiles.ConfigTile(TileType.PressurePlate, new PressurePlate());
		Tiles.ConfigTile(TileType.FalsePressurePlate, new FalsePressurePlate());
		Tiles.ConfigTile(TileType.PressurePlateReseter, new PressurePlateReseter());
		Tiles.ConfigTile(TileType.PressurePlateChecker, new PressurePlateChecker());
		Tiles.ConfigTile(TileType.ToggleFloorOn, new ToggleFloorOn());
		Tiles.ConfigTile(TileType.ToggleFloorOff, new ToggleFloorOff());
		Tiles.ConfigTile(TileType.Puzzle2Checker, new Puzzle2Checker());
	}
	public static void Init() { }
}
public sealed class SmokeFloor : Tile
{
	public SmokeFloor()
	{
		Name = "Floor";
		Description = "A flat floor";
		DetailedDescription = "A flat floor with a sigil on it, it looks like an eye of some sort";
		Walkable = true;
		BlocksVision = true;
		TileKind = TileKind.Walkable;
	}
}
public class Teleporter : Tile, IOnEnter
{
	public Teleporter()
	{
		Name = "Floor";
		Description = "A flat floor with a sigil of some sort";
		DetailedDescription = "A flat floor with a ";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config?.Targets is null || config.Targets.None()) return ErrorResult.ConfigMissing(pos);
		player.Position = config.Targets.First();
		return new GeneralResult("With a flash and a poof you find yourself somewhere new");
	}
}
public class PressurePlate : Tile, IOnEnter
{
	public PressurePlate()
	{
		Name = "Floor";
		Description = "Some sort of pressure plate";
		DetailedDescription = "Some sort of pressure plate";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config is null)
		{
			map.MapConfigs.Add(pos, new TileConfig());
			config = map.GetConfig(pos);
		}
		if (config is null) return ErrorResult.ConfigMissing(pos);
		if (config.Active) return new GeneralResult("*silence*");
		config.Active = true;
		return new GeneralResult("*click*");
	}
}
public class FalsePressurePlate : Tile, IOnEnter
{
	public FalsePressurePlate()
	{
		Name = "Floor";
		Description = "Some sort of pressure plate";
		DetailedDescription = "Some sort of pressure plate";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		return new GeneralResult("*silence*");
	}
}
public class PressurePlateReseter : Tile, IOnEnter
{
	public PressurePlateReseter()
	{
		Name = "Floor";
		Description = "Some sort of pressure plate, it has a marking of a circular arrow";
		DetailedDescription = "Some sort of pressure plate, it has a marking of a circular arrow";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config?.Targets is null || config.Targets.None()) return ErrorResult.ConfigMissing(pos);
		foreach (var target in config.Targets)
		{
			var targetConfig = map.GetConfig(target);
			if (targetConfig is null)
			{
				map.MapConfigs.Add(target, new TileConfig());
				targetConfig = map.GetConfig(target);
			}
			if (targetConfig is null) return ErrorResult.TargetConfigMissing(pos, target);
			targetConfig.Active = false;
		}
		return new GeneralResult("Loud clicks echo from inside the room");
	}
}
public class PressurePlateChecker : Tile, IOnEnter
{
	public PressurePlateChecker()
	{
		Name = "Floor";
		Description = "Some sort of pressure plate";
		DetailedDescription = "Some sort of pressure plate";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config?.Targets is null || config.Targets.None()) return ErrorResult.ConfigMissing(pos);
		foreach (var target in config.Targets)
		{
			var targetConfig = map.GetConfig(target);
			if (targetConfig is null)
			{
				map.MapConfigs.Add(target, new TileConfig());
				targetConfig = map.GetConfig(target);
			}
			if (targetConfig is null) return ErrorResult.TargetConfigMissing(pos, target);
			if (targetConfig.Active == false) return new GeneralResult("*silence*");
		}
		config.Active = true;
		return new SuccessResult("You hear a door unlocking across the room");
	}
}
public class Puzzle2Checker : Tile, IOnEnter
{
	public Puzzle2Checker()
	{
		Name = "Floor";
		Description = "Some sort of pressure plate";
		DetailedDescription = "Some sort of pressure plate";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config?.Targets is null || config.Targets.None()) return ErrorResult.ConfigMissing(pos);
		foreach (var target in config.Targets)
		{
			var targetConfig = map.GetTileAt(target);
			if (targetConfig is null) return ErrorResult.TargetConfigMissing(pos, target);
			if (targetConfig != TileType.ToggleFloorOn) return new GeneralResult("*silence*");
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