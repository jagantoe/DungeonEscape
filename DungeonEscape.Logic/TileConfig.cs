namespace DungeonEscape.Logic;
public sealed class TileConfig
{
	public ICollection<Vector2> Targets { get; set; }
	public bool Active { get; set; }
	public string? Text { get; set; }

	public void Toggle()
	{
		Active = !Active;
	}

	public Vector2 FirstTarget()
	{
		return Targets.First();
	}
}
