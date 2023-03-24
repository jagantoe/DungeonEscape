namespace DungeonEscape.PlayerClient.Models;
public class Tile
{
	public int PositionX { get; set; }
	public int PositionY { get; set; }
	public TileKind TileKind { get; set; }
}

public enum TileKind
{
	Walkable = 0,
	NonWalkable = 1,
	PointOfInterest = 2
}