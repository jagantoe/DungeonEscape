namespace DungeonEscape.Game.GameDTO;
public sealed class PlayerActionResultDTO
{
	public int PlayerId { get; set; }
	public IEnumerable<ActResult> ActionResults { get; set; }
}
public sealed class PlayerInspection
{
	public int PlayerId { get; set; }
	public Inspection Inspection { get; set; }
}
