namespace DungeonEscape.Logic;
public sealed class TileConfig
{
	public IEnumerable<Vector2> Targets { get; set; }
	public bool Active { get; set; }

	public void Toggle()
	{
		Active = !Active;
	}
}
