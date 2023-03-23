namespace DungeonEscape.Logic;
public class Player
{
	public int Id { get; set; }
	public string Name { get; set; }
	public PlayerCharacter Character { get; set; }
	public Vector2 Position { get; set; } = new(3, 3);
	public int CurrentHealth { get; set; }
	public int MaxHealth => MaxHealthCalc();
	public bool IsDead => CurrentHealth <= 0;
	public int Deaths { get; set; }
	public int Resets { get; set; }
	public List<Item> Items { get; set; }

	private int MaxHealthCalc()
	{
		var baseHealth = Character switch
		{
			PlayerCharacter.StrongMan => 15,
			PlayerCharacter.Explorer => 10,
			PlayerCharacter.Archeologist => 5,
			_ => 10
		};
		var health = baseHealth - Resets;
		if (health <= 0) health = 1;
		return health;
	}

	public int GetVisionRange()
	{
		var vision = Character == PlayerCharacter.Explorer ? 2 : 1;
		if (Items.Contains(Item.Lantern)) vision++;

		return vision;
	}

	public void Revive(Vector2 start)
	{
		Deaths += 1;
		CurrentHealth = MaxHealth;
		Position = start;
	}
	public void Reset(Vector2 start)
	{
		Resets += 1;
		Revive(start);
	}
}

public enum PlayerCharacter
{
	StrongMan = 0,
	Explorer = 1,
	Archeologist = 2
}
