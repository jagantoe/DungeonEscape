namespace DungeonEscape.PlayerClient.Models;
internal class Tile
{
	public int X { get; set; }
	public int Y { get; set; }
	public TileKind TileType { get; set; }
}

internal enum TileKind
{
	Walkable,
	NonWalkable,
	Interactable
}