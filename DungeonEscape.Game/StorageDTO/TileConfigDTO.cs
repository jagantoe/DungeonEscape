namespace DungeonEscape.Game.StorageDTO;

public sealed class TileConfigDTO
{
	public IEnumerable<string> Targets { get; set; }
	public bool Active { get; set; }
}
