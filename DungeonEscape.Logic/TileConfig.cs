namespace DungeonEscape.Logic;
public sealed class TileConfig
{
	public Vector2 Position { get; set; }
	public IEnumerable<Vector2> Targets { get; set; }
	public bool Active { get; set; }

	public void Toggle()
	{
		Active = !Active;
	}
}
