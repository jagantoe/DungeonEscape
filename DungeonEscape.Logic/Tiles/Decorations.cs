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
		Tiles.ConfigTile(TileType.Demon, new Demon());
		Tiles.ConfigTile(TileType.Vulture, new Vulture());
		Tiles.ConfigTile(TileType.Human, new Human());
	}
	public static void Init() { }
}
public sealed class Boulder : Decoration { public Boulder() : base("Boulder", "A large boulder") { } }
public sealed class Angel : Decoration { public Angel() : base("Angel Statue", "Statue of a weeping angel", TileKind.PointOfInterest) { } }
public sealed class Demon : Decoration { public Demon() : base("Demon Statue", "Statue of a dismembered demon", TileKind.PointOfInterest) { } }
public sealed class Vulture : Decoration { public Vulture() : base("Vulture Statue", "Statue of a vulture eating a corpse", TileKind.PointOfInterest) { } }
public sealed class Human : Decoration { public Human() : base("Human Statue", "Statue of a person curled up with their face hidden", TileKind.PointOfInterest) { } }
