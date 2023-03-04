namespace DungeonEscape.Game.GameDTO;
public sealed class PlayerGameStateDTO
{
	public PlayerDTO Player { get; set; }
	public List<TileDTO> Vision { get; set; }
}
