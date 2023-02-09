using System.Numerics;

namespace DungeonEscape.Logic;

public class Player
{
	public int Id { get; set; }
	public string Name { get; set; }
	public PlayerCharacter Character { get; set; }
	public Vector2 Position { get; set; } = new(3, 3);
	public int CurrentHealth { get; set; }
	public int MaxHealth { get; set; }
	public bool IsDead => CurrentHealth <= 0;
	public int Deaths { get; set; }
	public List<Item> Items { get; set; }

	public Queue<string> ActionResults { get; set; }

	public void Reset()
	{
		CurrentHealth = MaxHealth;
	}

	public void AddActionResult(string result)
	{
		if (string.IsNullOrWhiteSpace(result)) return;
		ActionResults.Enqueue(result);
	}
}
