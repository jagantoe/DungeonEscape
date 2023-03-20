namespace DungeonEscape.PlayerClient.Models;
internal class Tile
{
	public int PositionX { get; set; }
	public int PositionY { get; set; }
	public TileKind TileKind { get; set; }
}

internal enum TileKind
{
	Walkable,
	NonWalkable,
	PointOfInterest
}