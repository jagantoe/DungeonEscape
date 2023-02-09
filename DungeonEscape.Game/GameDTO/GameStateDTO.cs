namespace DungeonEscape.Game.GameDTO;
public class GameStateDTO
{
	public PlayerDTO Player { get; set; }
	public List<TileDTO> Vision { get; set; }
}
