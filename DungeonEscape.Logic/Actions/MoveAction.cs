using System.Numerics;

namespace DungeonEscape.Logic.Actions;
public class MoveAction : PlayerAction
{
	public Vector2 Target { get; set; }

	public MoveAction(int playerId, Vector2 target) : base(playerId)
	{
		Target = target;
	}
}
