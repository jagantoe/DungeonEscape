namespace DungeonEscape.Logic;
public sealed class SwitchCharacterAction : PlayerAction
{
	public PlayerCharacter Character { get; set; }
	public SwitchCharacterAction(int playerId, PlayerCharacter character) : base(playerId)
	{
		Character = character;
	}
	public override string ToString()
	{
		return $"Switch character to {Character}";
	}
}
