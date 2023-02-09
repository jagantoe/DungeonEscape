using DungeonEscape.Logic;

namespace DungeonEscape.Game.StorageDTO;
public class PlayerStorageDTO
{
	public int Id { get; set; }
	public string Name { get; set; }
	public PlayerCharacter Character { get; set; }
	public int PositionX { get; set; }
	public int PositionY { get; set; }

	public List<string> Items { get; set; }

	public int CurrentHealth { get; set; }
	public int MaxHealth { get; set; }
	public int Deaths { get; set; }
}
