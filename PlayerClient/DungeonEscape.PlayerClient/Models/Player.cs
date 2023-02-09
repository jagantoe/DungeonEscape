namespace DungeonEscape.PlayerClient.Models;
internal class Player
{
	public string Name { get; set; }
	public Character Character { get; set; }
	public int PositionX { get; set; }
	public int PositionY { get; set; }

	public List<string> Items { get; set; }

	public int CurrentHealth { get; set; }
	public int MaxHealth { get; set; }
	public int Deaths { get; set; }
}
