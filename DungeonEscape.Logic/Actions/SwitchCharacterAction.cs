namespace DungeonEscape.Logic.Actions;
public class SwitchCharacterAction : PlayerAction
{
	public PlayerCharacter Character { get; set; }
	public SwitchCharacterAction(int playerId, PlayerCharacter character) : base(playerId)
	{
		Character = character;
	}
}
