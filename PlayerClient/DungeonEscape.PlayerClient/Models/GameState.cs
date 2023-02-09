namespace DungeonEscape.PlayerClient.Models;
internal class GameState
{
	public Player Player { get; set; }
	public List<Tile> VisionTiles { get; set; }
}
