using DungeonEscape.Logic;

namespace DungeonEscape.Game.StorageDTO;
public class GameStorageDTO
{
	public int Id { get; set; }
	public bool Active { get; set; }
	public int CurrentRound { get; set; }
	public string MapName { get; set; }
	public Dictionary<string, TileType> GameState { get; set; }
	public List<PlayerStorageDTO> Players { get; set; }
}
