namespace DungeonEscape.Logic;

public static class Automation
{
	static Automation()
	{
		Tiles.ConfigTile(TileType.FloorMover, new FloorMover());
		Tiles.ConfigTile(TileType.WallMover, new WallMover());
		Tiles.ConfigTile(TileType.SpikeMover, new SpikeMover());
		Tiles.ConfigTile(TileType.FireMover, new FireMover());
	}
	public static void Init() { }
}

public abstract class TileMover : TileWithConfig, ITurnStart
{
	public TileType Type { get; set; }
	public TileMover(TileType type)
	{
		Type = type;
		Name = Floor.Name;
		Description = Floor.Description;
		DetailedDescription = Floor.Description;
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public void TurnStart(Vector2 pos, Map map)
	{
		var config = map.GetConfig(pos);
		if (config.MissingConfigTarget()) return;
		map.ClearState(config!.FirstTarget());
		config.Targets.Cycle();
		map.ChangeState(config.FirstTarget(), Type);
	}
}

public sealed class FloorMover : TileMover { public FloorMover() : base(TileType.Floor) { } }
public sealed class WallMover : TileMover { public WallMover() : base(TileType.Wall) { } }
public sealed class SpikeMover : TileMover { public SpikeMover() : base(TileType.VisibleSpikes) { } }
public sealed class FireMover : TileMover { public FireMover() : base(TileType.VisibleFire) { } }