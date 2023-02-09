using System.Numerics;

namespace DungeonEscape.Logic.Actions;
public class InteractAction : PlayerAction
{
	public Vector2 Target { get; set; }

	public InteractAction(int playerId, Vector2 target) : base(playerId)
	{
		Target = target;
	}
}
