namespace DungeonEscape.Game.GameDTO;
public sealed class TileDTO
{
	public int PositionX { get; set; }
	public int PositionY { get; set; }
	public TileKindDTO TileKind { get; set; }
}
