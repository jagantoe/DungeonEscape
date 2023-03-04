namespace DungeonEscape.Logic;
public abstract class PlayerAction
{
	public int PlayerId { get; set; }

	public PlayerAction(int playerId)
	{
		PlayerId = playerId;
	}
}
