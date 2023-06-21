namespace DungeonEscape.Logic;
public sealed class StandingAction : PlayerAction
{
	public StandingAction(int playerId) : base(playerId) { }
	public override string ToString()
	{
		return $"Standing still";
	}
}
