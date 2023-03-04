namespace DungeonEscape.Logic;
public abstract class Trap : Tile
{
	static Trap()
	{
		Tiles.ConfigTile(TileType.Spikes, new Wall());
		Tiles.ConfigTile(TileType.Pit, new CrackedWall());
		Tiles.ConfigTile(TileType.FirePlate, new BrokenWall());
		Tiles.ConfigTile(TileType.FalseFloor, new FalseFloor());
	}
	public static void Init() { }
}
public sealed class Spikes : Trap
{
	public Spikes()
	{
		Name = "Floor";
		Description = "A perforated floor";
		DetailedDescription = "A perforated floor, best to avoid it lest you also wish to be perforated";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.Walkable;
	}
}
public sealed class Pit : Trap
{
	public Pit()
	{
		Name = "Pit";
		Description = "A bottomless pit";
		DetailedDescription = "A bottomless pit and yet death waits below";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.Walkable;
	}
}
public sealed class FirePlate : Trap
{
	public FirePlate()
	{
		Name = "Floor";
		Description = "A slightly raised floor";
		DetailedDescription = "A scorching floor, best not stay too long";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
}
public sealed class FalseFloor : Trap
{
	public FalseFloor()
	{
		Name = "Floor";
		Description = "A flat floor";
		DetailedDescription = "It LOOKS like a flat floor";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
}