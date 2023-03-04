namespace DungeonEscape.Game.StorageDTO;
public sealed class PlayerStorageDTO
{
	public int Id { get; set; }
	public string Name { get; set; }
	public PlayerCharacter Character { get; set; }
	public int PositionX { get; set; }
	public int PositionY { get; set; }

	public List<string> Items { get; set; }

	public int CurrentHealth { get; set; }
	public int Deaths { get; set; }
	public int Resets { get; set; }
}
