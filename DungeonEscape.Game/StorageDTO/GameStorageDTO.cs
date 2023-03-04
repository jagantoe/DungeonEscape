namespace DungeonEscape.Game.StorageDTO;
public sealed class GameStorageDTO
{
	public int Id { get; set; }
	public bool Active { get; set; }
	public int CurrentRound { get; set; }
	public string MapName { get; set; }
	public Dictionary<string, TileType> GameState { get; set; }
	public List<PlayerStorageDTO> Players { get; set; }
}
