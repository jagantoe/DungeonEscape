namespace DungeonEscape.Logic;
public sealed class Start : Tile
{
	public Start()
	{
		Name = "Start";
		Description = "The start of the game";
		DetailedDescription = "The start of the game";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}

	static Start()
	{
		Tiles.ConfigTile(TileType.Start, new Start());
	}
	public static void Init() { }
}
