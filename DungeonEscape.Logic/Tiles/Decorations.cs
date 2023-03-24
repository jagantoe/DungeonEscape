namespace DungeonEscape.Logic;
public abstract class Decoration : Tile
{
	public Decoration(string name, string description, TileKind tileKind = TileKind.NonWalkable)
	{
		Name = name;
		Description = description;
		DetailedDescription = description;
		Walkable = false;
		BlocksVision = false;
		TileKind = tileKind;
	}

	static Decoration()
	{
		Tiles.ConfigTile(TileType.Boulder, new Boulder());
		Tiles.ConfigTile(TileType.Angel, new Angel());
	}
	public static void Init() { }
}
public sealed class Boulder : Decoration { public Boulder() : base("Boulder", "A large boulder") { } }
public sealed class Angel : Decoration { public Angel() : base("Angel Statue", "A statue of a weeping angel", TileKind.PointOfInterest) { } }
