namespace DungeonEscape.Logic.Actions;
public sealed class InteractAction : PlayerAction
{
	public Vector2 Target { get; set; }

	public InteractAction(int playerId, Vector2 target) : base(playerId)
	{
		Target = target;
	}
	public override string ToString()
	{
		return $"Action to interact with {Target}";
	}
}
