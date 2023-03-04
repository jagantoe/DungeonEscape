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
		Tiles.ConfigTile(TileType.CrackedWall, new CrackedWall());
		Tiles.ConfigTile(TileType.BrokenWall, new BrokenWall());
		Tiles.ConfigTile(TileType.IllusionWall, new IllusionWall());
	}
	public static void Init() { }
}
public sealed class IceFloor : Tile
{
	public IceFloor()
	{
		Name = "Ice";
		Description = "A cracked wall";
		DetailedDescription = "A cracked wall, perhaps with enough strength it can be broken";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
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