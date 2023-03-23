namespace DungeonEscape.Logic;
public sealed class ResetAction : PlayerAction
{
	public ResetAction(int playerId) : base(playerId)
	{

	}

	public override string ToString()
	{
		return "Reset back to the start";
	}
}
