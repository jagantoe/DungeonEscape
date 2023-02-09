namespace DungeonEscape.Logic;

public abstract class Engraving : Tile
{
	public Engraving(string description, string detailedDescription)
	{
		Name = $"Engraving";
		Description = description;
		DetailedDescription = detailedDescription;
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.Interactable;
	}

	static Engraving()
	{
		Tiles.ConfigTile(TileType.RedDoor, new RedDoor());
		Tiles.ConfigTile(TileType.BlueDoor, new BlueDoor());
		Tiles.ConfigTile(TileType.YellowDoor, new YellowDoor());
		Tiles.ConfigTile(TileType.PurpleDoor, new PurpleDoor());
		Tiles.ConfigTile(TileType.OpenDoor, new OpenDoor());
	}
}
public sealed class Puzzle1 : Engraving
{
	public Puzzle1() : base("Red", "") { }
}
public sealed class Puzzle2 : Engraving
{
	public Puzzle2() : base("Blue", "") { }
}
public sealed class Puzzle3 : Engraving
{
	public Puzzle3() : base("Yellow", "") { }
}
public sealed class Puzzle4 : Engraving
{
	public Puzzle4() : base("Purple", "") { }
}
public sealed class Puzzle5 : Engraving
{
	public Puzzle5() : base("Green", "") { }
}
