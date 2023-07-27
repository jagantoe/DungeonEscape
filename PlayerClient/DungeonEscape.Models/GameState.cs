namespace DungeonEscape.PlayerClient.Models;
public class GameState
{
	public Player Player { get; set; }
	public List<Tile> Vision { get; set; }
}
