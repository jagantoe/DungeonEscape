namespace DungeonEscape.Logic.Actions;
public sealed class ResetAction : PlayerAction
{
	public ResetAction(int playerId) : base(playerId)
	{

	}

	public override string ToString()
	{
		return "Action to reset back to the start";
	}
}
