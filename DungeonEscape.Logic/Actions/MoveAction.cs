namespace DungeonEscape.Logic;
public sealed class MoveAction : PlayerAction
{
	public Vector2 Target { get; set; }

	public MoveAction(int playerId, Vector2 target) : base(playerId)
	{
		Target = target;
	}
	public override string ToString()
	{
		return $"Move to {Target}";
	}
}
