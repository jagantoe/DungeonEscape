namespace DungeonEscape.Logic.Actions;
public sealed class InspectAction : PlayerAction
{
	public Vector2 Target { get; set; }

	public InspectAction(int playerId, Vector2 target) : base(playerId)
	{
		Target = target;
	}
	
	public override string ToString()
	{
		return $"Action to inspect {Target}";
	}
}
