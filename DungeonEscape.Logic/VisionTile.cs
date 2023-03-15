namespace DungeonEscape.Logic;
public sealed class VisionTile
{
	public VisionTile(Vector2 position, TileKind tileKind)
	{
		Position = position;
		TileKind = tileKind;
	}

	public Vector2 Position { get; set; }
	public TileKind TileKind { get; set; }
}
