namespace DungeonEscape.Logic;
public sealed class Wall : Tile
{
	public Wall()
	{
		Name = "Wall";
		Description = "It's just a wall";
		DetailedDescription = Description;
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.NonWalkable;
	}

	static Wall()
	{
		Tiles.ConfigTile(TileType.Wall, new Wall());
		Tiles.ConfigTile(TileType.CrackedWall, new CrackedWall());
		Tiles.ConfigTile(TileType.BrokenWall, new BrokenWall());
		Tiles.ConfigTile(TileType.IllusionWall, new IllusionWall());
		Tiles.ConfigTile(TileType.SecretWall, new SecretWall());
	}
	public static void Init() { }
}
public sealed class CrackedWall : Tile
{
	public CrackedWall()
	{
		Name = "Wall";
		Description = "A cracked wall";
		DetailedDescription = "A cracked wall, perhaps with enough strength it can be broken";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.NonWalkable;
	}
}
public sealed class BrokenWall : Tile
{
	public BrokenWall()
	{
		Name = "Broken wall";
		Description = "It's just a wall";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.NonWalkable;
	}
}
public sealed class IllusionWall : Tile
{
	public IllusionWall()
	{
		Name = "Wall";
		Description = "It's just a wall";
		DetailedDescription = "There seems to be a breeze flowing from the wall";
		Walkable = true;
		BlocksVision = true;
		TileKind = TileKind.NonWalkable;
	}
}
public sealed class SecretWall : Tile
{
	public SecretWall()
	{
		Name = "Wall";
		Description = "It's just a wall";
		DetailedDescription = Description;
		Walkable = true;
		BlocksVision = true;
		TileKind = TileKind.NonWalkable;
	}
}
